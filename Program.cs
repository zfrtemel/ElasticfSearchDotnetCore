using ElasticfSearchDotnetCore.Business.Interfaces;
using ElasticfSearchDotnetCore.Business.Services;
using ElasticfSearchDotnetCore.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer("Data Source=localhost; Initial Catalog=ElasticDB; Integrated Security=True; Connect Timeout=30; Encrypt=False; Trust Server Certificate=False; Application Intent=ReadWrite; Multi Subnet Failover=False");
});
var settings = new ConnectionSettings(new Uri("https://localhost:9200"))
     .ServerCertificateValidationCallback((o, certificate, chain, errors) => true) // Güvenlik riski taþýr, sadece geliþtirme/test amaçlý kullanýlmalýdýr
     .BasicAuthentication("elastic", "ng9J*WFY7rGAP0hr7U*a")
    .DefaultIndex("products");
var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);

builder.Services.AddScoped<IElasticSearchManager, ElasticSearchManager>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
