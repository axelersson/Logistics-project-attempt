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
public class LocationsController : ControllerBase
{
    private readonly LogisticsDBContext _context; // Replace with your actual DbContext
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(LogisticsDBContext context, ILogger<LocationsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _context.Locations.ToListAsync();
        return Ok(locations);
    }

    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetLocationById(string locationId)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);

        if (location == null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] Location location)
    {
        if (location == null)
        {
            return BadRequest();
        }

        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLocationById), new { locationId = location.LocationId }, location);
    }

    [HttpPut("{locationId}")]
    public async Task<IActionResult> UpdateLocation(string locationId, [FromBody] Location updatedLocation)
    {
        if (locationId != updatedLocation.LocationId)
        {
            return BadRequest();
        }

        _context.Entry(updatedLocation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LocationExists(locationId))
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

    [HttpDelete("{locationId}")]
    public async Task<IActionResult> DeleteLocation(string locationId)
    {
        var location = await _context.Locations.FindAsync(locationId);

        if (location == null)
        {
            return NotFound();
        }

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LocationExists(string locationId)
    {
        return _context.Locations.Any(l => l.LocationId == locationId);
    }
}
