using KiltaSphereAPI.Data;
using Microsoft.EntityFrameworkCore;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Repositories;
using KiltaSphereAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get the connection string from appsettings.Development.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the database context service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Define CORS policy for the frontend application
const string FrontendCorsPolicy = "FrontendCorsPolicy";

var frontendUrl = builder.Configuration["FrontendUrl"] ?? "http://localhost:5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.WithOrigins(frontendUrl)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers the repository
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

// Register the Service (The business logic layer)
builder.Services.AddScoped<IMemberService, MemberService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(FrontendCorsPolicy); // Enable the middleware

app.UseAuthorization();

app.MapControllers();

app.Run();
