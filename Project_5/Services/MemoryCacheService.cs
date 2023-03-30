using Microsoft.Extensions.Caching.Memory;
using Project_5.Interfaces.Services;
using Project_5.Models;

namespace Project_5.Services;

public class MemoryCacheService: IMemoryService
{
    #region Поля

    private IMemoryCache _memoryCache;
    private IUserService _userService;

    #endregion

    #region Конструктор

    public MemoryCacheService(IMemoryCache memoryCache, IUserService userService)
    {
        _memoryCache = memoryCache;
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
        
        _memoryCache.TryGetValue("cacheCollectionUsers", out tempCollectionUsers);
        if (tempCollectionUsers == null)
        {
            tempCollectionUsers = await _userService.DeserializeJsonFileAsync();
            _memoryCache.Set("cacheCollectionUsers", tempCollectionUsers, 
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }

        return tempCollectionUsers;
    }

    #endregion
}