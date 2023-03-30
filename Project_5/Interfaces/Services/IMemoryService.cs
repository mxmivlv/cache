using Project_5.Models;

namespace Project_5.Interfaces.Services;

public interface IMemoryService
{
    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public Task<IEnumerable<User>> GetUsersAsync();
}