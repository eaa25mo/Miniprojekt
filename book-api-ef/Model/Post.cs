namespace Model;

public enum PostType
{
    Text,
    Url
};
public class Post 
{
    public int Id { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>(); 
    public DateTime Created { get; set; }
    public int UserId { get; set; }
    public User? Author { get; set; }
    public string Title { get; set; } = "";
    public PostType Type { get; set; }
    public string Content { get; set; } = ""; //url or text impliet by Type
    public Post()
    {
        Id = 0;
        Title = "";
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
        Author = null;
    }
}
