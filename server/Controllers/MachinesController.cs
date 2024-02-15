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
public class MachinesController : ControllerBase
{
    private readonly LogisticsDBContext _context; 
    private readonly ILogger<MachinesController> _logger;

    public MachinesController(LogisticsDBContext context, ILogger<MachinesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MachinesGetAllResponse))]
    public async Task<IActionResult> GetMachines()
    {
        var machines = await _context.Machines.ToListAsync();
        return Ok(new MachinesGetAllResponse { Machines = machines });
    }   

    [HttpGet("{machineId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMachineById(string machineId)
    {
        var machine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == machineId);

        if (machine == null)
        {
            return NotFound();
        }

        return Ok(machine);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Machine))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMachine([FromBody] Machine machine)
    {
        if (machine == null)
        {
            return BadRequest();
        }

        _context.Machines.Add(machine);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMachineById), new { machineId = machine.MachineId }, machine);
    }

    [HttpPut("{machineId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMachine(string machineId, [FromBody] Machine updatedMachine)
    {
        if (machineId != updatedMachine.MachineId)
        {
            return BadRequest();
        }

        _context.Entry(updatedMachine).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MachineExists(machineId))
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

    [HttpDelete("{machineId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMachine(string machineId)
    {
        var machine = await _context.Machines.FindAsync(machineId);

        if (machine == null)
        {
            return NotFound();
        }

        _context.Machines.Remove(machine);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MachineExists(string machineId)
    {
        return _context.Machines.Any(m => m.MachineId == machineId);
    }
}
