namespace Model;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } ="";
    public User()
    {
        Id = 0;
        Username = string.Empty;
    }
}