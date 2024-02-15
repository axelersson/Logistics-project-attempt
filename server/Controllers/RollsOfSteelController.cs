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
public class RollsOfSteelController : ControllerBase
{
    private readonly LogisticsDBContext _context; 
    private readonly ILogger<RollsOfSteelController> _logger;

    public RollsOfSteelController(LogisticsDBContext context, ILogger<RollsOfSteelController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RollsOfSteelGetAllResponse))]
    public async Task<IActionResult> GetRollsOfSteel()
    {
        var rollsOfSteel = await _context.RollsOfSteel.ToListAsync();
        return Ok(new RollsOfSteelGetAllResponse { RollsOfSteel = rollsOfSteel });
    }

    [HttpGet("{rollOfSteelId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RollOfSteel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRollOfSteelById(string rollOfSteelId)
    {
        var rollOfSteel = await _context.RollsOfSteel.FirstOrDefaultAsync(r => r.RollOfSteelId == rollOfSteelId);

        if (rollOfSteel == null)
        {
            return NotFound();
        }

        return Ok(rollOfSteel);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RollOfSteel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRollOfSteel([FromBody] RollOfSteel rollOfSteel)
    {
        if (rollOfSteel == null)
        {
            return BadRequest();
        }

        _context.RollsOfSteel.Add(rollOfSteel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRollOfSteelById), new { rollOfSteelId = rollOfSteel.RollOfSteelId }, rollOfSteel);
    }

    [HttpPut("{rollOfSteelId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRollOfSteel(string rollOfSteelId, [FromBody] RollOfSteel updatedRollOfSteel)
    {
        if (rollOfSteelId != updatedRollOfSteel.RollOfSteelId)
        {
            return BadRequest();
        }

        _context.Entry(updatedRollOfSteel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RollOfSteelExists(rollOfSteelId))
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

    [HttpDelete("{rollOfSteelId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRollOfSteel(string rollOfSteelId)
    {
        var rollOfSteel = await _context.RollsOfSteel.FindAsync(rollOfSteelId);

        if (rollOfSteel == null)
        {
            return NotFound();
        }

        _context.RollsOfSteel.Remove(rollOfSteel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RollOfSteelExists(string rollOfSteelId)
    {
        return _context.RollsOfSteel.Any(r => r.RollOfSteelId == rollOfSteelId);
    }
}
