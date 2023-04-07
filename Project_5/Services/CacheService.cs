using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Project_5.Interfaces.Services;
using Project_5.Models;

namespace Project_5.Services;

public class CacheService: IMemoryService
{
    #region Поля

    private IDistributedCache _cache;
    private IUserService _userService;

    #endregion

    #region Конструктор

    public CacheService(IDistributedCache cache, IUserService userService)
    {
        _cache = cache;
        _userService = userService;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Получение пользователей и добавления их в кэш памяти
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        IEnumerable<User>? tempCollectionUsers = null;
        
        var bytes = _cache.Get("cacheCollectionUsers");
        
        if (bytes != null)
        {
            var json = Encoding.UTF8.GetString(bytes);
            tempCollectionUsers = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
        }
        else
        {
            tempCollectionUsers = await _userService.DeserializeJsonFileAsync();
            var json = JsonConvert.SerializeObject(tempCollectionUsers);
            bytes = Encoding.UTF8.GetBytes(json); 
            _cache.Set("cacheCollectionUsers", bytes, 
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
        }

        return tempCollectionUsers;
    }

    #endregion
}