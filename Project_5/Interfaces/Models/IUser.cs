namespace Project_5.Interfaces.Models;

public interface IUser
{
    public int Id { get; set; }
    public string Surname { get; set; }
    
    public string Name { get; set; }
    
    public string Patronymic { get; set; }
    
    public int Age { get; set; }
}