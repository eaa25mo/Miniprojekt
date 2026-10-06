namespace Model;

public class Comment
{
    public int Id { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public string Content { get; set; } = "";
    public int UserId { get; set; }
    public User? Author { get; set; }
    public DateTime Created { get; set; }
}