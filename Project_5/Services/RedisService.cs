using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Project_5.Interfaces.Services;
using Project_5.Models;

namespace Project_5.Services;

public class RedisService: IMemoryService
{
    #region Поля

    private IDistributedCache _distributedCache;
    private IUserService _userService;

    #endregion

    #region Конструктор

    public RedisService(IDistributedCache distributedCache, IUserService userService)
    {
        _distributedCache = distributedCache;
        _userService = userService;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Получение пользователей и добавления их в кэш Redis
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        IEnumerable<User>? tempCollectionUsers = null;
        
        var usersString = await _distributedCache.GetStringAsync("cacheCollectionUsers");
        if (usersString != null)
        {
            tempCollectionUsers = JsonConvert.DeserializeObject<List<User>>(usersString);
        }
        else
        {
            tempCollectionUsers = await _userService.DeserializeJsonFileAsync();

            usersString = JsonConvert.SerializeObject(tempCollectionUsers);
            await _distributedCache.SetStringAsync("cacheCollectionUsers", usersString , new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }

        return tempCollectionUsers;
    }

    #endregion
}