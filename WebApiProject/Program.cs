using Microsoft.EntityFrameworkCore;
using WebApiProject.ApplicationServices.Services;
using WebApiProject.ApplicationServices.Services.Contracts;
using WebApiProject.Models;
using WebApiProject.Models.Services.Contracts;
using WebApiProject.Models.Services.Repositories;


#region [- Building the app object -]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<WebApiProjectDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")
));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();


var app = builder.Build();
#endregion



#region [- adding middlewares in the road of running -]
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
#endregion


app.Run();
