using Microsoft.OpenApi.Models;
using InventApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "InventApp API", Version = "v1" });
});

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("InventApp"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventApp API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
