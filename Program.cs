using Drones_Api.Configuration;
using Drones_Api.Data;
using Drones_Api.Repository;
using Drones_Api.Repository.IRepository;
using DronesAPI.Models;
using DronesAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DronesDB>(options => options.UseInMemoryDatabase("DronesDB"));
builder.Services.AddAutoMapper(typeof(MapperInitializer));
builder.Services.AddScoped<IDroneRepository, DroneRepository>();
builder.Services.AddScoped<IDroneLogsRepository, DroneLogsRepository>();
builder.Services.AddTransient<IHostedService, DroneBatteryHostedService>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

var app = builder.Build();

var scope = app.Services.CreateScope();
DatabaseInitializer.SeedData(scope.ServiceProvider.GetRequiredService<DronesDB>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")
    ),
    RequestPath = "/StaticFiles",
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
