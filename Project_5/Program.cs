using Project_5.Interfaces.Services;
using Project_5.Services;

var builder = WebApplication.CreateBuilder(args);

//Перменная для переключения кэша
var flagCache = true;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// My DI container
builder.Services.AddTransient<IUserService, UserService>();

if (flagCache)
{
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddStackExchangeRedisCache(options => {
        options.Configuration = "localhost";
        options.InstanceName = "local";
        });
    builder.Services.AddTransient<IMemoryService, RedisService>();
}
else
{
    builder.Services.AddMemoryCache();
    builder.Services.AddTransient<IMemoryService, MemoryCacheService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();