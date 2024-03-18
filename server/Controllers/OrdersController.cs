using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LogisticsApp.Data;

[ApiController]
[Route("/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly LogisticsDBContext _context;
    private readonly ILogger<OrdersController> _logger;

    private readonly ILoggerService _loggingService;

    public OrdersController(LogisticsDBContext context, ILogger<OrdersController> logger, ILoggerService loggingService)
    {
        _context = context;
        _logger = logger;
        _loggingService = loggingService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersGetAllResponse))]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders.Include(o => o.TruckOrderAssignments).ToListAsync();
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



    // CREATE NEW ORDER
    // Required body attributes: FromLocId, ToLocId, UserId, Pieces, OrderType
    [HttpPost("NewOrder/{userId}")]
    public async Task<IActionResult> CreateOrder(string userId, [FromBody] Order order)
    {
        //get ToLoc And FromLoc from order body
        var fromLoc = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == order.FromLocId);
        var toLoc = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == order.ToLocId);
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

        // order.OrderId = Guid.NewGuid().ToString();
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
        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.OrderId == orderId);

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
        var truckOrderAssignments = await _context.TruckOrderAssignments
                    .Where(toa => toa.OrderId == orderId)
                    .ToListAsync();

        if (truckOrderAssignments == null)
        {
            return BadRequest("Order not assigned to a truck");
        }

        order.OrderStatus = OrderStatus.Delivered;
        order.CompletedAt = DateTime.Now;

        foreach (var truckOrderAssignment in truckOrderAssignments)
        {
            truckOrderAssignment.IsAssigned = false;
            truckOrderAssignment.UnassignedAt = DateTime.Now;
            _context.Entry(truckOrderAssignment).State = EntityState.Modified;
        }

        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }

    // PARTIALLY DELIVER ORDER
    [HttpPut("PartialDeliver/{orderId}/Pieces/{deliveredPieces}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PartiallyDeliverOrder(string orderId, int deliveredPieces)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return BadRequest("Order not found");
        }

        // Logic to completely deliver order if deliveredPieces > pieces
        Console.WriteLine("Delivered pieces", order.DeliveredPieces);
        order.DeliveredPieces = deliveredPieces;
        Console.WriteLine(order.DeliveredPieces);
        if (order.DeliveredPieces >= order.Pieces)
        {
            Console.WriteLine("Order Delivered!");
            order.OrderStatus = OrderStatus.Delivered;
            order.CompletedAt = DateTime.Now;

            //Unassign trucks assigned to order if completelty delivered
            var truckOrderAssignments = await _context.TruckOrderAssignments
                    .Where(toa => toa.OrderId == orderId)
                    .ToListAsync();

            if (truckOrderAssignments != null)
            {
                foreach (var truckOrderAssignment in truckOrderAssignments)
                {
                    truckOrderAssignment.IsAssigned = false;
                    truckOrderAssignment.UnassignedAt = DateTime.Now;
                    _context.Entry(truckOrderAssignment).State = EntityState.Modified;
                }
            }

        }
        else
        {
            Console.WriteLine("Order Partially Delivered!");
            order.OrderStatus = OrderStatus.PartiallyDelivered;
        }

        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }

    //UNASSIGN ALL TRUCKS FROM ORDER
    [HttpPut("Unassign/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignOrder(string orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return BadRequest("Order not found");
        }

        // Logic to completely deliver order if deliveredPieces > pieces
        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.OrderId == orderId);

        if (truckOrderAssignment == null)
        {
            return BadRequest("Order not assigned to a truck");
        }

        truckOrderAssignment.IsAssigned = false;
        truckOrderAssignment.UnassignedAt = DateTime.Now;
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
    }

    //UNASSIGN ONE TRUCKS FROM ORDER
    [HttpPut("Unassign/{orderId}/Truck/{truckId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignAllTrucks(string orderId, string truckId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return BadRequest("Order not found");
        }

        // Logic to completely deliver order if deliveredPieces > pieces
        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.OrderId == orderId && toa.TruckId == truckId);

        if (truckOrderAssignment == null)
        {
            return BadRequest("Order not assigned to a truck");
        }

        truckOrderAssignment.IsAssigned = false;
        truckOrderAssignment.UnassignedAt = DateTime.Now;
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

        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.OrderId == orderId);
        if (truckOrderAssignment != null)
        {
            truckOrderAssignment.UnassignedAt = DateTime.Now;
            truckOrderAssignment.IsAssigned = false;
            _context.Entry(truckOrderAssignment).State = EntityState.Modified;
        }

        order.OrderStatus = OrderStatus.Cancelled;

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

    //Get all orders assigned to a truck
    [HttpGet("TruckOrders/{truckId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersGetAllResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTruckOrders(string truckId)
    {
        var truckOrderAssignments = await _context.TruckOrderAssignments.Where(tu => tu.IsAssigned == true && tu.TruckId == truckId).ToListAsync();

        if (truckOrderAssignments == null)
        {
            return NotFound();
        }

        return Ok(new TruckOrderAssignmentsGetAllResponse { TruckOrderAssignments = truckOrderAssignments });
    }

    //Change the IsAssigned property into True
    [HttpPut("AssignTruckOrder/{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // 添加成功响应的类型
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // 添加请求错误响应的类型
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 添加未找到资源响应的类型
    public async Task<IActionResult> AssignTruckOrder(string orderId)
    {
        // 检查传入的 orderId 是否为空
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return BadRequest("OrderId must be provided.");
        }

        // 查找与 orderId 相关联的 TruckOrderAssignment 实体
        var truckOrderAssignment = await _context.TruckOrderAssignments
                                                 .FirstOrDefaultAsync(t => t.OrderId == orderId);

        // 检查是否找到相应的 TruckOrderAssignment 实体
        if (truckOrderAssignment == null)
        {
            return NotFound($"TruckOrderAssignment with OrderId {orderId} not found.");
        }

        // 将 IsAssigned 属性设置为 true
        truckOrderAssignment.IsAssigned = true;

        // 更新分配时间
        truckOrderAssignment.AssignedAt = DateTime.Now;

        // 提交到数据库
        _context.TruckOrderAssignments.Update(truckOrderAssignment);
        await _context.SaveChangesAsync();

        // 返回成功响应
        return Ok(truckOrderAssignment);
    }



    private bool OrderExists(string orderId)
    {
        return _context.Orders.Any(o => o.OrderId == orderId);
    }
}
