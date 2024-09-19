using com.btc.dataaccess.Context;
using com.btc.dataaccess.Generic.Abstract;
using com.btc.dataaccess.Generic.Concrete;
using com.btc.process.manager.System.Abstract;
using com.btc.process.manager.System.Concrete;
using com.btc.process.utility.redis.Abstract;
using com.btc.process.utility.redis.Concrete;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration configuration = builder.Configuration;
var redisConnection = ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false");
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
builder.Services.AddDbContext<ExampleContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserManager, UserManager>();

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
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
app.UseCors("default");
app.MapControllers();

app.Run();
