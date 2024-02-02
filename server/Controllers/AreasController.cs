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
    private readonly LogisticsDBContext _context; // Replace with your actual DbContext
    private readonly ILogger<AreasController> _logger;

    public AreasController(LogisticsDBContext context, ILogger<AreasController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAreas()
    {
        var areas = await _context.Areas.ToListAsync();
        return Ok(areas);
    }

    [HttpGet("{areaId}")]
    public async Task<IActionResult> GetAreaById(string areaId)
    {
        var area = await _context.Areas.FirstOrDefaultAsync(a => a.AreaId == areaId);

        if (area == null)
        {
            return NotFound();
        }

        return Ok(area);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArea([FromBody] Area area)
    {
        if (area == null)
        {
            return BadRequest();
        }

        _context.Areas.Add(area);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAreaById), new { areaId = area.AreaId }, area);
    }

    [HttpPut("{areaId}")]
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
    public async Task<IActionResult> DeleteArea(string areaId)
    {
        var area = await _context.Areas.FindAsync(areaId);

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
