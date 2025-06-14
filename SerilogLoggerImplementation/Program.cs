using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Serilog:
// - Read configuration from appsettings.json using builder.Configuration.
// - Set up logging to the console and to a file.
Log.Logger = new LoggerConfiguration()// Create a new Serilog logger configuration
    .ReadFrom.Configuration(builder.Configuration) // Read settings from appsettings.json 
    .WriteTo.Console() // Log output to the console
    .WriteTo.File("Logs/SeriLog.txt") // Log output to a file
    .CreateLogger(); // Build the logger

// Replace the default logging provider with Serilog
builder.Host.UseSerilog(); // This ensures Serilog handles all logging

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
