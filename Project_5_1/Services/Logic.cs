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

    #region Методы

    /// <summary>
    /// Метод запуска логики программы
    /// </summary>
    public void Start()
    {
        Console.WriteLine("Нужно выбрать, кем быть: 1 - слушатель, 2 - отправитель.");

        try
        {
            var intUser = Convert.ToInt32(Console.ReadLine());
            if (intUser == 1)
            {
                Console.WriteLine("Вы вошли как слушатель. Ждем сообщения...");
                Subscriber();
            }
            if (intUser == 2)
            {
                Console.WriteLine("Вы вошли как отправитель. " +
                                  "Можете вводить сообщения и отправлять. " +
                                  "Для выхода введите: 0");
                Publisher();
            }
        }
        catch (Exception e)
        {
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
    }
    
    /// <summary>
    /// Метод слушателя
    /// </summary>
    private void Subscriber()
    {
        _subscriber.Subscribe("channel", (channel, message) => {
            Console.WriteLine((string)message);
        });
        Console.ReadLine();
    }
    
    /// <summary>
    /// Метод отправителя
    /// </summary>
    private void Publisher()
    {
        while (true)
        {
            string message = Console.ReadLine();
            if (message == "0")
                break;
            else
                _subscriber.Publish("channel", message);
        }
    }

    #endregion
}