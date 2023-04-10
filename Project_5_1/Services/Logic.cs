using StackExchange.Redis;

namespace Project_5_1;

public class Logic
{
    #region Поля

    private ConnectionMultiplexer _connection;
    private ISubscriber _subscriber;

    #endregion

    #region Конструктор

    public Logic()
    {
        Connect();
    }

    #endregion

    #region Метод

    /// <summary>
    /// Метод запуска логики программы
    /// </summary>
    public void Start()
    {
        string message = String.Empty;
        try
        {
            while (true)
            {
                message = Console.ReadLine();
                if (String.IsNullOrEmpty(message)) 
                {
                    Console.WriteLine("Вы вышли из программы");
                    break;
                }
                else
                {
                    Console.WriteLine($"Сообщение {message} отправлено");
                    _subscriber.Publish("channel", message);
                }
                    
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Сообщение {message} не доставлено");
            Console.WriteLine(e);
        }
    }

    /// <summary>
    /// Метод подключения
    /// </summary>
    private void Connect()
    {
        _connection = ConnectionMultiplexer.Connect("localhost");
        _subscriber = _connection.GetSubscriber();
        
        _subscriber.Subscribe("channel", (channel, message) => {
            Console.WriteLine($"Сообщение получено: {(string)message}");
        });
    }
    #endregion
}