using Project_5.Models;

namespace Project_5.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    /// Прочитать файл json с диска и десереализовать в коллекцию объектов
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public Task<IEnumerable<User>> DeserializeJsonFileAsync();
}