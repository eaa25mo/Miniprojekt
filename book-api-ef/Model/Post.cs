namespace Model;

public class Post 
{
    public int PostId { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>(); 
    public DateTime Created { get; set; }
    public User author { get; set; }
    public string Title { get; set; }
    public string? Url { get; set; }
    public string? Text { get; set; }
}