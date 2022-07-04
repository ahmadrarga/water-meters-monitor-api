using Microsoft.EntityFrameworkCore;
using WaterMetersMonitor.Api.Extensions;
using WaterMetersMonitor.Api.Registrations;
using WaterMetersMonitor.Infrastructure.DataContexts;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", builder => builder.WithOrigins("http://lovalhost:4200"));
});
services.AddDbContext<SqlDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DomainDB")));
services
    .AddRepositories()
    .AddServices()
    .AddMapper();

var app = builder.Build();

//app.UseApiExceptions();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

