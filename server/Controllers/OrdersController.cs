using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApp.Data; // Import your DbContext namespace
using Microsoft.Extensions.Logging; // Import for logging

// TODO: Need to add rolls of steel to the order 
// and add them to the OrderRollsOfSteel table
// when making an order

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
        var order = await _context.Orders
        .Include(o => o.OrderRolls)
            .ThenInclude(or => or.RollOfSteel)
        .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // Get all rolls of steel associated with an order
    [HttpGet("{orderId}/rolls")]
    public async Task<IActionResult> GetOrderRolls(string orderId)
    {
    var order = await _context.Orders
        .Select(o => new { OrderId = o.OrderId, DestinationId = o.DestinationId }) // Only select the fields we need
        .FirstOrDefaultAsync(o => o.OrderId == orderId);

    if (order == null)
    {
        return NotFound();
    }

    var orderRolls = await _context.OrderRolls
        .Where(or => or.OrderId == orderId)
        .ToListAsync();

    var rollOfSteelIds = orderRolls.Select(or => or.RollOfSteelId).ToList();

    var rollsOfSteel = await _context.RollsOfSteel
        .Where(roll => rollOfSteelIds.Contains(roll.RollOfSteelId))
        .ToListAsync();

    return Ok(new { Order = order, RollsOfSteel = rollsOfSteel });
}

    [HttpPost("user/{userId}/location/{locationId}")]
    public async Task<IActionResult> CreateOrder(string locationId, string userId, [FromBody] List<RollOfSteel> rolls)
    {
        if (rolls == null)
        {
            return BadRequest();
        }

        //get location from locationId
        var location = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
        if (location == null)
        {
            return BadRequest("Location not found");
        }
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        Order order = new Order();

        order.DestinationLocation = location;
        order.OrderId = Guid.NewGuid().ToString();
        order.OrderStatus = OrderStatus.Pending;
        order.User = user;

        foreach (var roll in rolls)
        {
            var orderRoll = new OrderRoll();
            orderRoll.Order = order;
            orderRoll.RollOfSteelId = roll.RollOfSteelId;
            orderRoll.OrderRollStatus = OrderRollStatus.Pending;
            _context.OrderRolls.Add(orderRoll);
        }
        order.DestinationId = locationId;
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

    private bool OrderExists(string orderId)
    {
        return _context.Orders.Any(o => o.OrderId == orderId);
    }
}
