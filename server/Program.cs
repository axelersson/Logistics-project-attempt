using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster i containern.
// Läs mer om att konfigurera Swagger/OpenAPI på https://aka.ms/aspnetcore/swashbuckle


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
        Credential = GoogleCredential.FromFile("C:/Users/axele/OneDrive/Dokument/GitHub/Logistics-project-attempt/server/env/testingdotnetandfirebase-firebase-adminsdk-gck0a-56846fdb9d.json")
    });
    Console.WriteLine("Firebase initialized successfully.");
}
catch (Exception ex)
{
    // Skriv ut felmeddelande om initialisering av Firebase misslyckas
    Console.WriteLine($"Error initializing Firebase: {ex.Message}");
}

// Lägg till API Explorer och Swagger Generator
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<FirebaseService>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddLogging();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Lägg till andra nödvändiga tjänster som kontroller
// builder.Services.AddControllers(); Behövs detta?

var app = builder.Build();

// Konfigurera HTTP-begäran pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseHttpsRedirection();
    app.UseDeveloperExceptionPage(); // Use developer exception page to see detailed errors
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
app.UseExceptionHandler(a => a.Run(async context =>
{
    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = feature?.Error; // Use null-conditional operator
    var message = exception?.Message ?? "An unknown error occurred"; // Fallback in case of null
    
    var result = JsonConvert.SerializeObject(new { error = message });
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(result);
}));
}



//app.UseHttpsRedirection();

// Aktivera CORS för begäranden som matchar "AllowAngularDevOrigin"-policyn.
// Detta tillåter din webbapplikation att kommunicera med frontend i Angular
// även om de körs på olika servrar eller portar under utveckling.
app.UseCors("AllowAngularDevOrigin");

// I Configure-metoden för ASP.NET Core 3.1
app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Map controller routes



app.Run();
