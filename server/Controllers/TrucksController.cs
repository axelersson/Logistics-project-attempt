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
public class TrucksController : ControllerBase
{
    private readonly LogisticsDBContext _context;
    private readonly ILogger<TrucksController> _logger;

    private readonly ILoggerService _loggingService;

    public TrucksController(LogisticsDBContext context, ILogger<TrucksController> logger, ILoggerService loggingService)
    {
        _context = context;
        _logger = logger;
        _loggingService = loggingService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrucksGetAllResponse))]
    public async Task<IActionResult> GetTrucks()
    {
        var trucks = await _context.Trucks.Include(t => t.TruckUsers.Where(t => t.IsAssigned == true)).ToListAsync();
        return Ok(new TrucksGetAllResponse { Trucks = trucks });

    }


    [HttpGet("{truckId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Truck))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTruckById(string truckId)
    {
        var truck = await _context.Trucks.Include(tu => tu.TruckUsers).FirstOrDefaultAsync(t => t.TruckId == truckId);

        if (truck == null)
        {
            return NotFound();
        }

        return Ok(truck);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Truck))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTruck([FromBody] Truck truck)
    {
        if (truck == null)
        {
            return BadRequest();
        }

        _context.Trucks.Add(truck);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTruckById), new { truckId = truck.TruckId }, truck);
    }

    [HttpPut("{truckId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTruck(string truckId, [FromBody] Truck updatedTruck)
    {
        if (truckId == null)
        {
            return BadRequest();
        }
        updatedTruck.TruckId = truckId;
        _context.Entry(updatedTruck).State = EntityState.Modified;

        try
        {
            // Save changes to the database
            await _context.SaveChangesAsync();

            // Retrieve the updated truck from the database
            var updatedEntity = await _context.Trucks.FindAsync(truckId);

            if (updatedEntity == null)
            {
                return NotFound();
            }

            // Return the updated truck in the response
            return Ok(updatedEntity);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TruckExists(truckId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }

    [HttpDelete("{truckId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTruck(string truckId)
    {
        Console.WriteLine("Hello");
        var truck = await _context.Trucks.FindAsync(truckId);

        var truckOrderAssignments = await _context.TruckOrderAssignments
            .Where(toa => toa.TruckId == truckId)
            .ToListAsync();

        var truckUsers = await _context.TruckUsers
            .Where(tu => tu.TruckId == truckId)
            .ToListAsync();


        if (truck == null)
        {
            return NotFound();
        }

        foreach (var truckOrderAssignment in truckOrderAssignments)
        {
            truckOrderAssignment.IsAssigned = false;
            truckOrderAssignment.TruckId = null;
            truckOrderAssignment.UnassignedAt = DateTime.Now;
            _context.Entry(truckOrderAssignment).State = EntityState.Modified;
        }

        foreach (var truckUser in truckUsers)
        {
            truckUser.IsAssigned = false;
            truckUser.TruckId = null;
            truckUser.UnassignedAt = DateTime.Now;
            _context.Entry(truckUser).State = EntityState.Modified;
        }
        _context.Trucks.Remove(truck);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ASSIGN TRUCK TO USER
    [HttpPost("{truckId}/AssignUser/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignTruckToUser(string truckId, string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var truck = await _context.Trucks.FirstOrDefaultAsync(t => t.TruckId == truckId);

        if (truck == null)
        {
            return BadRequest("Truck not found");
        }

        TruckUser truckUser = new TruckUser();

        truckUser.UserId = userId;
        truckUser.TruckId = truckId;
        truckUser.AssignedAt = DateTime.Now;
        truckUser.IsAssigned = true;

        _context.TruckUsers.Add(truckUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTruckUserById), new { truckUserId = truckUser.TruckUserId }, truckUser);
    }

    // UNASSIGN TRUCK
    [HttpPost("Unassign/{truckId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UnassignTruck(string truckId)
    {
        var truckUser = await _context.TruckUsers.FirstOrDefaultAsync(tu => tu.TruckId == truckId && tu.IsAssigned == true);

        if (truckUser == null)
        {
            return BadRequest("Truck not found or not assigned");
        }

        truckUser.IsAssigned = false;
        truckUser.UnassignedAt = DateTime.Now;

        _context.Entry(truckUser).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //GET TRUCKUSER BY ID
    [HttpGet("TruckUser/{truckUserId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TruckUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTruckUserById(int truckUserId)
    {
        var truckUser = await _context.TruckUsers.FirstOrDefaultAsync(tu => tu.TruckUserId == truckUserId);

        if (truckUser == null)
        {
            return NotFound();
        }

        return Ok(truckUser);
    }

    // GET ALL ASSIGNED TRUCKUSERS
    [HttpGet("AssignedTruckUsers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TruckUsersGetAllResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAssignedTruckUsers()
    {
        var truckUsers = await _context.TruckUsers.Where(tu => tu.IsAssigned == true).ToListAsync();

        if (truckUsers == null)
        {
            return NotFound();
        }

        return Ok(new TruckUsersGetAllResponse { TruckUsers = truckUsers });
    }

    // ASSIGN TRUCK TO ORDER
    [HttpPost("{truckId}/AssignOrder/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> AssignTruckToOrder(string truckId, string orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null)
        {
            return BadRequest("Order not found");
        }
        var truck = await _context.Trucks.FirstOrDefaultAsync(t => t.TruckId == truckId);

        if (truck == null)
        {
            return BadRequest("Truck not found");
        }

        TruckOrderAssignment truckOrderAssignment = new TruckOrderAssignment();

        truckOrderAssignment.OrderId = orderId;
        truckOrderAssignment.TruckId = truckId;
        truckOrderAssignment.AssignedAt = DateTime.Now;

        _context.TruckOrderAssignments.Add(truckOrderAssignment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTruckOrderAssignmentById), new { truckOrderAssignmentId = truckOrderAssignment.TruckOrderAssignmentId }, truckOrderAssignment);
    }

    // GET TRUCKORDERASSIGNMENT BY ID
    [HttpGet("TruckOrderAssignment/{truckOrderAssignmentId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TruckOrderAssignment))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetTruckOrderAssignmentById(int truckOrderAssignmentId)
    {
        var truckOrderAssignment = await _context.TruckOrderAssignments.FirstOrDefaultAsync(toa => toa.TruckOrderAssignmentId == truckOrderAssignmentId);

        if (truckOrderAssignment == null)
        {
            return NotFound();
        }

        return Ok(truckOrderAssignment);
    }

    

    [HttpGet("GetTruckIdByUserId/{userId}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> GetTruckIdByUserId(string userId)
{
    // 查找与给定 UserId 相关联的 TruckUser
    var truckUser = await _context.TruckUsers.FirstOrDefaultAsync(tu => tu.UserId == userId && tu.IsAssigned);

    if (truckUser == null)
    {
        // 如果找不到匹配的 TruckUser，返回 NotFound 响应
        return NotFound($"No truck assigned to user with ID {userId}.");
    }

    // 如果找到了，返回 TruckId
    return Ok(new { truckUser.TruckId });
}




    private bool TruckExists(string truckId)
    {
        return _context.Trucks.Any(t => t.TruckId == truckId);
    }
}
