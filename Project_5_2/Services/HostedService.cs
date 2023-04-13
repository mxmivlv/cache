using Project_5_2.Interfaces.Services;

namespace Project_5_2.Services;

public class HostedService: BackgroundService
{
    #region Поля

    private IMemoryService _memoryService;

    #endregion

    #region Конструктор

    public HostedService(IMemoryService memoryService)
    {
        _memoryService = memoryService;
    }
    
    #endregion

    #region Метод

    /// <summary>
    /// Метод работающий в фоне, добавляет пользователей в кэш
    /// </summary>
    /// <param name="cancellationToken">Токен для отменты</param>
    /// <returns>Возвращает задачу</returns>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await _memoryService.AddUserInRedisAsync();
            }
        });
 
        return Task.CompletedTask;
    }

    #endregion
}