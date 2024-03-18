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
public class AreasController : ControllerBase
{
    private readonly LogisticsDBContext _context;
    private readonly ILogger<AreasController> _logger;

    private readonly ILoggerService _loggingService;

    public AreasController(LogisticsDBContext context, ILogger<AreasController> logger, ILoggerService loggingService)
    {
        _context = context;
        _logger = logger;
        _loggingService = loggingService;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AreasResponse))] // Note the changed type here
    public async Task<IActionResult> GetAreas()
    {
        var areas = await _context.Areas.ToListAsync();
        var response = new AreasResponse { Areas = areas };
        return Ok(response);
    }


    [HttpGet("{areaId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Area))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAreaById(string areaId)
    {
        var area = await _context.Areas.Include(a => a.Locations).FirstOrDefaultAsync(a => a.AreaId == areaId);

        if (area == null)
        {
            return NotFound();
        }

        return Ok(area);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Area))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateArea([FromBody] Area area)
    {
        if (area == null)
        {
            return BadRequest();
        }

        _context.Areas.Add(area);
        await _context.SaveChangesAsync();
        Console.WriteLine("Created area: ", area);
        return CreatedAtAction(nameof(GetAreaById), new { areaId = area.AreaId }, area);
    }

    [HttpPut("{areaId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateArea(string areaId, [FromBody] Area updatedArea)
    {
        if (areaId != updatedArea.AreaId)
        {
            return BadRequest();
        }

        _context.Entry(updatedArea).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AreaExists(areaId))
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

    [HttpDelete("{areaId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArea(string areaId)
    {
        var area = await _context.Areas.FindAsync(areaId);
        Console.WriteLine(area);
        if (area == null)
        {
            return NotFound();
        }

        _context.Areas.Remove(area);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AreaExists(string areaId)
    {
        return _context.Areas.Any(a => a.AreaId == areaId);
    }
}
