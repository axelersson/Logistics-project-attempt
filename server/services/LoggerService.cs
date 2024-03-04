using Microsoft.Extensions.Logging;
using LogisticsApp.Data; // This imports the namespace where LogisticsDBContext is defined
using System;

public class LoggerService : ILoggerService
{
    private readonly ILogger<LoggerService> _logger;
    private readonly LogisticsDBContext _context;


    public LoggerService(ILogger<LoggerService> logger, LogisticsDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message); // Optionally keep this to also log to the configured providers

        var logEntry = new LogEntry { Timestamp = DateTime.UtcNow, Message = message };
        _context.LogEntries.Add(logEntry);
        _context.SaveChanges();
    }
}
