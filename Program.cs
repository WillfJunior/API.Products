using API.Products.Infra;
using API.Products.Infra.Repository;
using API.Products.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IProductRepsository, ProductRepository>();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionRedis");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/product", async (IProductRepsository _repo) =>
{
    var products = await _repo.GetProducts();

    return products;
})
.WithTags("Product");

app.MapPost("/product", async (IProductRepsository _repo, Product products) =>
{
    var product = await _repo.CreateProduct(products);

    return product;
})
.WithTags("Product");

app.Run();

