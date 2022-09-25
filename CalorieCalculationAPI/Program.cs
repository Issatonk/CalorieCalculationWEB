using CalorieCalculation.BusinessLogic;
using CalorieCalculation.Core.Repositories;
using CalorieCalculation.Core.Services;
using CalorieCalculation.DataAccess.Sqlite;
using CalorieCalculation.DataAccess.Sqlite.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<CalorieCalculationContext>(options =>
{
    options.UseSqlite(builder.Configuration["DefaultConnection"]);
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DataAccessMappingProfile>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
