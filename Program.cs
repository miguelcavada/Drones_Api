using Drones_Api.Data;
using DronesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DronesDB>(options => options.UseInMemoryDatabase("DronesDB"));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
