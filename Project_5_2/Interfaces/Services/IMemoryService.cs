using Project_5_2.Models;

namespace Project_5_2.Interfaces.Services;

public interface IMemoryService
{
    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public Task AddUserInRedisAsync();
}