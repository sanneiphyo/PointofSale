using PointOfSale.Domain.Features.ProductCategory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DI

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
} ,ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductCategoryService>();

builder.Services.AddScoped<SaleService>();


#endregion

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
