using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using LogisticsApp.Data; // Ensure this is the correct namespace for your DbContext
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevOrigin", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200", "http://localhost:4201")
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


//DotNetEnv.Env.Load();
//Console.WriteLine($"Direct Access JWT Secret: {Environment.GetEnvironmentVariable("JWT_Secret")}");

// Use the secret for configuring authentication, JWT bearer options, etc.
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Setup your token validation parameters, and use jwtSecret as needed
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument();
builder.Services.AddAuthorization();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddSingleton<ITokenService, TokenService>();



builder.Services.AddDbContext<LogisticsDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseOpenApi();
}
else
{
    app.UseExceptionHandler(a => a.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = feature?.Error;
        var message = exception?.Message ?? "An unknown error occurred";
        
        var result = JsonConvert.SerializeObject(new { error = message });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }));
}

app.UseCors("AllowAngularDevOrigin");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
