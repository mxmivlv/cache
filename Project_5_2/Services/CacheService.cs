using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Project_5_2.Interfaces.Services;
using Project_5_2.Models;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Project_5.Services;

public class CacheService: IMemoryService
{
    #region Поля

    private static int _indexUser;
    private IDistributedCache _cache;
    private RedLockFactory _redLockFactory;

    #endregion

    #region Конструктор

    static CacheService()
    {
        _indexUser++;
    }

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
        Connect();
    }

    #endregion

    #region Методы

    /// <summary>
    /// Добавления пользователя в кэш
    /// </summary>
    public async Task AddUserInRedisAsync()
    {
        // Ресурс который блокируется
        var resource = "local";
        
        // Максимальное время блокировки при сбое
        var expiry = TimeSpan.FromSeconds(30);
        
        // Время блокировки ресурса
        var wait = TimeSpan.FromSeconds(5);
        
        // Попытки получить доступ
        var retry = TimeSpan.FromSeconds(1);

        await using (var redLock = await _redLockFactory.CreateLockAsync(resource, expiry, wait, retry))
        {
            Console.WriteLine($"Поток: {redLock.LockId} хочет записать данные. Разрешение на запись: {redLock.IsAcquired}");
            if (redLock.IsAcquired)
            {
                Console.WriteLine($"Поток: {redLock.LockId} получил разрешение на запись");
                
                // Получаем json файл
                var json = JsonConvert.SerializeObject(new User
                (
                    $"Фамилия_{_indexUser}", 
                    $"Имя_{_indexUser}", 
                    $"Отчетсво_{_indexUser}",
                    _indexUser)
                );
                
                // Получаем массив байтов из json файла
                var bytes = Encoding.UTF8.GetBytes(json); 
                
                // Добавляем в кэш
                _cache.Set($"User_{_indexUser}", bytes, 
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                _indexUser++;
                //Thread.Sleep(3000);
            }
        }
    }
    
    /// <summary>
    /// Подключение к Redis
    /// </summary>
    private void Connect()
    {
        // Создаем подключение
        var _connection = ConnectionMultiplexer.Connect("localhost");

        // Добавляем подключение
        var multiplexers = new List<RedLockMultiplexer>
        {
            _connection,
        };
        
        _redLockFactory = RedLockFactory.Create(multiplexers);
    }

    #endregion
}