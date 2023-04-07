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
builder.Services.AddTransient<IMemoryService, CacheService>();

if (flagCache)
{
    builder.Services.AddStackExchangeRedisCache(options => {
        options.Configuration = "localhost";
        options.InstanceName = "local";
    });
}
else
{
    builder.Services.AddDistributedMemoryCache();
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