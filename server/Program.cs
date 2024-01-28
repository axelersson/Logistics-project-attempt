using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevOrigin", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200", "http://localhost:4201") // Ports for Angular apps
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

/* // Initialize Firebase
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("C:/Users/axele/OneDrive/Dokument/GitHub/Logistics-project-attempt/server/env/testingdotnetandfirebase-firebase-adminsdk-gck0a-56846fdb9d.json")
});
 */

try
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile("C:/Users/axele/OneDrive/Dokument/GitHub/Logistics-project-attempt/server/env/testingdotnetandfirebase-firebase-adminsdk-gck0a-56846fdb9d.json")
    });
    Console.WriteLine("Firebase initialized successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing Firebase: {ex.Message}");
}



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add other necessary services like controllers
//builder.Services.AddControllers(); need this?


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDevOrigin");

// In Configure method for ASP.NET Core 3.1
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // Map attribute-routed controllers
});


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
