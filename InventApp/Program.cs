using Microsoft.OpenApi.Models;
using InventApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventApp API", Version = "v1" });
});

builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("InventApp"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
