using Microsoft.Extensions.Configuration;
using ZelisCabPortalBusinessLayer.Implementations;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalDataLayer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<IAuth,Auth>();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<ICabService,CabService>();
builder.Services.AddScoped<IManager,Manager>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
