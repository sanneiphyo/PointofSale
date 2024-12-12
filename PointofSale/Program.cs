using Microsoft.EntityFrameworkCore;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Features.Product;
using PointOfSale.Domain.Features.Sale;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
} ,ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<SaleDetailsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
