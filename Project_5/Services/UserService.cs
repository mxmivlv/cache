using Newtonsoft.Json;
using Project_5.Interfaces.Services;
using Project_5.Models;

namespace Project_5.Services;

public class UserService: IUserService
{
    #region Поля

    private readonly string jsonFileUsers = "CollectionUsers.json"; 

    #endregion

    #region Метод

    /// <summary>
    /// Прочитать файл json с диска и десереализовать в коллекцию объектов
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public async Task<IEnumerable<User>> DeserializeJsonFileAsync()
    {
        var usersString = await File.ReadAllTextAsync(jsonFileUsers);
        var collectionUsers = JsonConvert.DeserializeObject<List<User>>(usersString);
        
        return collectionUsers;
    }

    #endregion
}