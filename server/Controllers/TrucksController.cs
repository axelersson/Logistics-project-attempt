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
    private readonly LogisticsDBContext _context; // Replace with your actual DbContext
    private readonly ILogger<TrucksController> _logger;

    public TrucksController(LogisticsDBContext context, ILogger<TrucksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrucks()
    {
        var trucks = await _context.Trucks.ToListAsync();
        return Ok(trucks);
    }

    [HttpGet("{truckId}")]
    public async Task<IActionResult> GetTruckById(string truckId)
    {
        var truck = await _context.Trucks.FirstOrDefaultAsync(t => t.TruckId == truckId);

        if (truck == null)
        {
            return NotFound();
        }

        return Ok(truck);
    }

    [HttpPost]
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
    public async Task<IActionResult> UpdateTruck(string truckId, [FromBody] Truck updatedTruck)
    {
        if (truckId != updatedTruck.TruckId)
        {
            return BadRequest();
        }

        _context.Entry(updatedTruck).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
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

        return NoContent();
    }

    [HttpDelete("{truckId}")]
    public async Task<IActionResult> DeleteTruck(string truckId)
    {
        var truck = await _context.Trucks.FindAsync(truckId);

        if (truck == null)
        {
            return NotFound();
        }

        _context.Trucks.Remove(truck);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TruckExists(string truckId)
    {
        return _context.Trucks.Any(t => t.TruckId == truckId);
    }
}
