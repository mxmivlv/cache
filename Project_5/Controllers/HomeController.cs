using Microsoft.AspNetCore.Mvc;
using Project_5.Interfaces.Services;
using Project_5.Models;

namespace Project_5.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HomeController: ControllerBase
{
    #region Поля
    
    private IMemoryService _memoryService;

    #endregion

    #region Конструктор

    public HomeController(IMemoryService memoryService)
    {
        _memoryService = memoryService;
    }

    #endregion

    [HttpGet(Name = "GetUsers") ]
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _memoryService.GetUsersAsync();
    }
}