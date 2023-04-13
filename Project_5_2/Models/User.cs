namespace Project_5_2.Models;

public class User
{
    #region Свойства
    
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }

    #endregion

    #region Конструктор
    
    public User(string surname, string name, string patronymic, int age)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Age = age;
    }

    #endregion
}