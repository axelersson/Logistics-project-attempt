using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApp.Data; // Import your DbContext namespace
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Internal; // Import for logging

// TODO: Need to add rolls of steel to the order 
// and add them to the OrderRollsOfSteel table
// when making an order

[ApiController]
[Route("/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly LogisticsDBContext _context; // Replace with your actual DbContext
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(LogisticsDBContext context, ILogger<OrdersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(string orderId)
    {
        var order = await _context.Orders
        .Include(o => o.OrderRolls)
        .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
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
