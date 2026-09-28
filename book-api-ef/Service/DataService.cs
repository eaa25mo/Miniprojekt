using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private BookContext db { get; }

    public DataService(BookContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData() {
        
      

        db.SaveChanges();
    }

    
    public List<Post> GetPosts() {
        return db.Posts.Include(p => p.User).ToList();
    }
    
    public Post GetPost(int id) {
        return db.Posts.Include(p => p.User).FirstOrDefault(p => p.PostId == id);
    }
    
    public string updateDownVotePost(int postId) {
        Post post = db.Posts.FirstOrDefault(t => t.PostId == postId);
       

        if (post == null) {
            return "post not found";
        }


        post.Downvote--;
        
        db.SaveChanges();
        return "Vote updated";
    }
    public string updateUpvotePost(int postId) {
        Post post = db.Posts.FirstOrDefault(t => t.PostId == postId);
       

        if (post == null) {
            return "post not found";
        }


        post.Upvote++;
        
        db.SaveChanges();
        return "Vote updated"; 
    }
    
    
    public string updateDownVoteComment(int commentId) {
        Comment comment = db.Comment.FirstOrDefault(t => t.CommentId == commentId);
       

        if (comment == null) {
            return "Comment not found";
        }


        comment.Downvote--;
        
        db.SaveChanges();
        return "Comment updated";
    }
    public string updateUpvoteComment(int commmentId) {
        Comment comment = db.comments.FirstOrDefault(t => t.CommentId == commentd);
       

        if (comment == null) {
            return "comment not found";
        }


        comment.Upvote++;
        
        db.SaveChanges();
        return "comment updated"; 
    }
 
    public string CreatePost(string title, int userId) {
        User user = db.Users.FirstOrDefault(a => a.UserId == userId);
        db.Posts.Add(new Post { Title = title, User = user });
        db.SaveChanges();
        return "Post created";
    }
    public string CreateComment(string text, int userId) {
        User user = db.Users.FirstOrDefault(a => a.UserId == userId);
        db.Posts.Comment.Add(new Comment { Text = text, User = user });
        db.SaveChanges();
        return "Comment created";
    }
}
