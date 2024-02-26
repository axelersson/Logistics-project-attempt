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
    private readonly LogisticsDBContext _context; 
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(LogisticsDBContext context, ILogger<LocationsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationsGetAllResponse))]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _context.Locations.ToListAsync();
        return Ok(new LocationsGetAllResponse { Locations = locations });
    }
    
    [HttpGet("{locationId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Location))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Location))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLocation([FromBody] Location location)
    {
        if (location == null)
    {
        return BadRequest();
    }

    if (await _context.Locations.AnyAsync(l => l.LocationId == location.LocationId))
    {
        // 如果 locationId 已存在，则返回一个具体的错误消息
        return BadRequest("Location ID already exists.");
    }

    var area = await _context.Areas.FindAsync(location.AreaId);
    if (area == null)
    {
        // 如果找不到对应的 areaId，则返回一个具体的错误消息
        return BadRequest("Area ID does not exist.");
    }

    location.Area = area; // 确保 location 与区域关联
    _context.Locations.Add(location);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetLocationById), new { locationId = location.LocationId }, location);
    }

    [HttpPost("Area/{areaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddLocationsToArea(string areaId, [FromBody] List<Location> locations)
    {
        if (locations == null || locations.Count == 0)
        {
            return BadRequest("No locations provided.");
        }

        var area = await _context.Areas.FirstOrDefaultAsync(a => a.AreaId == areaId);
        if (area == null)
        {
            return NotFound("Area not found.");
        }

        foreach (var location in locations)
        {
            if (!string.IsNullOrEmpty(location.AreaId))
            {
                return BadRequest("Locations should not already be associated with an area.");
            }

            location.Area = area;
            _context.Locations.Add(location);
        }

        await _context.SaveChangesAsync();

        return Ok("Locations added to the area successfully.");
    }

    [HttpPut("{locationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
