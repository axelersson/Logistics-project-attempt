using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApp.Data; // Import your DbContext namespace
using Microsoft.Extensions.Logging; // Import for logging

[ApiController]
[Route("api/[controller]")]
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        if (order == null)
        {
            return BadRequest();
        }

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
