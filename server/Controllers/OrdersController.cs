using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApp.Data; // Import your DbContext namespace
using Microsoft.Extensions.Logging; // Import for logging


[ApiController]
[Route("/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly LogisticsDBContext _context; 
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(LogisticsDBContext context, ILogger<OrdersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersGetAllResponse))]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return Ok(new OrdersGetAllResponse { Orders = orders });
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(string orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // Get all rolls of steel associated with an order
    // REMOVED
//     [HttpGet("{orderId}/rolls")]
//     public async Task<IActionResult> GetOrderRolls(string orderId)
//     {
//     var order = await _context.Orders
//         .Select(o => new { OrderId = o.OrderId, DestinationId = o.DestinationId }) // Only select the fields we need
//         .FirstOrDefaultAsync(o => o.OrderId == orderId);

//     if (order == null)
//     {
//         return NotFound();
//     }

//     var orderRolls = await _context.OrderRolls
//         .Where(or => or.OrderId == orderId)
//         .ToListAsync();

//     var rollOfSteelIds = orderRolls.Select(or => or.RollOfSteelId).ToList();

//     var rollsOfSteel = await _context.RollsOfSteel
//         .Where(roll => rollOfSteelIds.Contains(roll.RollOfSteelId))
//         .ToListAsync();

//     return Ok(new { Order = order, RollsOfSteel = rollsOfSteel });
// }


    // CREATE NEW ORDER
    // Required body attributes: FromLocId, ToLocId, UserId, Pieces, OrderType
    [HttpPost("NewOrder/{userId}")]
    public async Task<IActionResult> CreateOrder(string userId, [FromBody] Order order)
    {
        //get ToLoc And FromLoc from order body
        Console.WriteLine("FromLocId: " + order.FromLocId);
        Console.WriteLine("ToLocId: " + order.ToLocId);
        var fromLoc = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == order.FromLocId);
        var toLoc = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == order.ToLocId);
        Console.WriteLine("FromLoc: " + fromLoc);
        Console.WriteLine("ToLoc: " + toLoc);
        if (fromLoc == null || toLoc == null)
        {
            return BadRequest("Location not found");
        }
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        order.ToLocation = toLoc;
        order.FromLocation = fromLoc;

        order.OrderId = Guid.NewGuid().ToString();
        order.OrderStatus = OrderStatus.Pending;
        order.User = user;

        order.CreatedAt = DateTime.Now;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }


    [HttpPut("{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(string orderId, [FromBody] Order updatedOrder)
    {
        if (orderId != updatedOrder.OrderId)
        {
            return BadRequest();
        }

        _context.Entry(updatedOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(orderId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(string orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELIVER ORDER 
    [HttpPut("Deliver/{orderId}")]
    public async Task<IActionResult> DeliverOrder(string orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return BadRequest("Order not found");
        }
        // Unassign truck from order
        // Maybe this should change all assigned trucks, not just the first one?
        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.OrderId == orderId);
        
        if (truckOrderAssignment == null)
        {
            return BadRequest("Order not assigned to a truck");
        }

        order.OrderStatus = OrderStatus.Delivered;
        order.CompletedAt = DateTime.Now;

        truckOrderAssignment.UnassignedAt = DateTime.Now;
        truckOrderAssignment.IsAssigned = false;

        _context.Entry(truckOrderAssignment).State = EntityState.Modified;
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }

    // PARTIALLY DELIVER ORDER
    [HttpPut("PartialDeliver/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> PartiallyDeliverOrder(string orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return BadRequest("Order not found");
        }

        order.OrderStatus = OrderStatus.PartiallyDelivered;
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }

    // CANCEL ORDER
    [HttpPut("Cancel/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> CancelOrder(string orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return BadRequest("Order not found");
        }
        // Unassign truck from order
        // Maybe this should change all assigned trucks, not just the first one?
        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.OrderId == orderId);
        if (truckOrderAssignment != null)
        {
            truckOrderAssignment.UnassignedAt = DateTime.Now;
            truckOrderAssignment.IsAssigned = false;
            _context.Entry(truckOrderAssignment).State = EntityState.Modified;
        }

        order.OrderStatus = OrderStatus.Cancelled;
        truckOrderAssignment.UnassignedAt = DateTime.Now;
        truckOrderAssignment.IsAssigned = false; 

        _context.Entry(truckOrderAssignment).State = EntityState.Modified;
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }

    // GET ALL TRUCKORDERASSIGNMENTS
    [HttpGet("Assignments")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TruckOrderAssignmentsGetAllResponse))]
    public async Task<IActionResult> GetTruckOrderAssignments()
    {
        var truckOrderAssignments = await _context.TruckOrderAssignments.ToListAsync();
        return Ok(new TruckOrderAssignmentsGetAllResponse { TruckOrderAssignments = truckOrderAssignments });
    }

    private bool OrderExists(string orderId)
    {
        return _context.Orders.Any(o => o.OrderId == orderId);
    }
}
