using FarmasiCaseStudy.Business.Abstract;
using FarmasiCaseStudy.Business.Concrete;
using FarmasiCaseStudy.Core.Repository.Abstract;
using FarmasiCaseStudy.Core.Settings;
using FarmasiCaseStudy.DataAccess.Abstract;
using FarmasiCaseStudy.DataAccess.Concrete;
using FarmasiCaseStudy.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoSettings>(options =>
{
    options.ConnectionString = builder.Configuration["MongoConnection:ConnectionString"];
    options.Database = builder.Configuration["MongoConnection:Database"];
});
builder.Services.Configure<RedisSettings>(options => {
    options.ConnectionString = builder.Configuration["RedisConnection:ConnectionString"];
});
builder.Services.AddDistributedRedisCache(options =>
{
    options.InstanceName = "";
    options.Configuration = builder.Configuration["RedisConnection:ConnectionString"];
});
builder.Services.AddSession(options => {
    options.Cookie.Name = "TestCookie";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
// Add services to the container.
// Create Scope
builder.Services.AddScoped(typeof(IDataGenericRepository<>), typeof(MongoRepositoryBase<>));
builder.Services.AddScoped(typeof(ICacheGenericRepository), typeof(RedisCacheRepositoryBase));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder =>
builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
