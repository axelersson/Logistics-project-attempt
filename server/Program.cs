using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster i containern.
// Läs mer om att konfigurera Swagger/OpenAPI på https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    // Definiera en CORS-policy med namnet "AllowAngularDevOrigin".
    // Detta tillåter begäranden från specificerade Angular-utvecklingsportar.
    options.AddPolicy("AllowAngularDevOrigin", policyBuilder =>
    {
        // Tillåt begäranden från Angular-utvecklingsservrar på portarna 4200 och 4201.
        // Tillåt alla HTTP-metoder och alla headers från dessa källor.
        policyBuilder.WithOrigins("http://localhost:4200", "http://localhost:4201") 
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

try
{
    // Skapa en Firebase-app med angivna inställningar
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile("F:/Linus/Arbete/GIK2PG/Project/Logistics-project-attempt/server/env/linus-thing-firebase-adminsdk-v0ftt-066a98d462.json")
    });
    Console.WriteLine("Firebase initialized successfully.");
}
catch (Exception ex)
{
    // Skriv ut felmeddelande om initialisering av Firebase misslyckas
    Console.WriteLine($"Error initializing Firebase: {ex.Message}");
}

// Lägg till API Explorer och Swagger Generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lägg till andra nödvändiga tjänster som kontroller
// builder.Services.AddControllers(); Behövs detta?

var app = builder.Build();

// Konfigurera HTTP-begäran pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aktivera CORS för begäranden som matchar "AllowAngularDevOrigin"-policyn.
// Detta tillåter din webbapplikation att kommunicera med frontend i Angular
// även om de körs på olika servrar eller portar under utveckling.
app.UseCors("AllowAngularDevOrigin");

// I Configure-metoden för ASP.NET Core 3.1
app.UseRouting();

app.Run();
