namespace Model;

public class Comment
{
    public int CommentId { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public string Text { get; set; }
    public User author { get; set; }
    public DateTime created { get; set; }
    
}