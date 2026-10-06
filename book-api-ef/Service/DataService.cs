using Data;
using Microsoft.EntityFrameworkCore;
using Model;
using NuGet.Protocol;
using System.ComponentModel.Design;
using System.Text.Json;

namespace Service;

public class DataService
{
    private PostContext db { get; }

    public DataService(PostContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {
        // Users
        User alice = new User
        {
            Username = "alice"
        };

        User bob = new User
        {
            Username = "bob"
        };

        User charlie = new User
        {
            Username = "charlie"
        };

        User david = new User
        {
            Username = "david"
        };

        User emma = new User
        {
            Username = "emma"
        };

        db.Users.AddRange(alice, bob, charlie, david, emma);


        // Comments
        Comment c1 = new Comment
        {
            Content = "I really like C# for backend development.",
            Upvotes = 12,
            Downvotes = 1,
            UserId = 2,
            Author = bob,
            Created = new DateTime(2026, 1, 10, 12, 30, 0)
        };

        Comment c2 = new Comment
        {
            Content = "Python is also a great choice, especially for beginners.",
            Upvotes = 8,
            Downvotes = 2,
            UserId = 3,
            Author = charlie,
            Created = new DateTime(2026, 1, 10, 13, 15, 0)
        };

        Comment c3 = new Comment
        {
            Content = "I started with JavaScript and then moved to TypeScript.",
            Upvotes = 5,
            Downvotes = 0,
            UserId = 4,
            Author = david,
            Created = new DateTime(2026, 1, 10, 14, 0, 0)
        };

        Comment c4 = new Comment
        {
            Content = "This is a very useful resource. Thanks!",
            Upvotes = 15,
            Downvotes = 0,
            UserId = 1,
            Author = alice,
            Created = new DateTime(2026, 1, 11, 10, 0, 0)
        };

        Comment c5 = new Comment
        {
            Content = "The examples in the documentation are especially good.",
            Upvotes = 7,
            Downvotes = 1,
            UserId = 5,
            Author = emma,
            Created = new DateTime(2026, 1, 11, 11, 30, 0)
        };

        Comment c6 = new Comment
        {
            Content = "Welcome! Looking forward to seeing more posts from you.",
            Upvotes = 3,
            Downvotes = 0,
            UserId = 2,
            Author = bob,
            Created = new DateTime(2026, 1, 12, 9, 15, 0)
        };

        Comment c7 = new Comment
        {
            Content = "I don't really agree with this opinion.",
            Upvotes = 1,
            Downvotes = 6,
            UserId = 3,
            Author = charlie,
            Created = new DateTime(2026, 1, 12, 10, 45, 0)
        };


        // Posts
        Post p1 = new Post
        {
            Title = "What is your favorite programming language?",
            Type = PostType.Text,
            Content = "I've been learning programming lately. What language do you recommend?",
            Upvotes = 42,
            Downvotes = 3,
            Created = new DateTime(2026, 1, 10, 10, 0, 0),
            Author = alice,
            Comments = new List<Comment>
        {
            c1,
            c2,
            c3
        }
        };

        Post p2 = new Post
        {
            Title = "Useful C# documentation",
            Type = PostType.Url,
            Content = "https://learn.microsoft.com/en-us/dotnet/csharp/",
            Upvotes = 65,
            Downvotes = 2,
            Created = new DateTime(2026, 1, 11, 9, 0, 0),
            Author = bob,
            Comments = new List<Comment>
        {
            c4,
            c5
        }
        };

        Post p3 = new Post
        {
            Title = "My first post!",
            Type = PostType.Text,
            Content = "Hello everyone! Happy to join this community.",
            Upvotes = 18,
            Downvotes = 0,
            Created = new DateTime(2026, 1, 12, 8, 30, 0),
            Author = charlie,
            Comments = new List<Comment>
        {
            c6
        }
        };

        Post p4 = new Post
        {
            Title = "What do you think about TypeScript?",
            Type = PostType.Text,
            Content = "I've been considering learning TypeScript. Is it worth learning in 2026?",
            Upvotes = 27,
            Downvotes = 5,
            Created = new DateTime(2026, 1, 13, 15, 0, 0),
            Author = david,
            Comments = new List<Comment>
        {
            c7
        }
        };

        // Important test case: a post with NO comments
        Post p5 = new Post
        {
            Title = "Interesting programming article",
            Type = PostType.Url,
            Content = "https://example.com/programming",
            Upvotes = 3,
            Downvotes = 4,
            Created = new DateTime(2026, 1, 14, 16, 0, 0),
            Author = emma,
            Comments = new List<Comment>()
        };


        db.Posts.AddRange(p1, p2, p3, p4, p5);

        db.SaveChanges();
    }


    public List<Post> GetPosts() {
        return db.Posts.Include(p => p.Author).ToList();
    }
    
    public Post? GetPost(int id) {
        return db.Posts.Include(p => p.Author).Include(p => p.Comments).ToList().FirstOrDefault(p => p.Id == id);
    }
    public string UpdateUpvotePost(int postId) {
        Post? post = db.Posts.FirstOrDefault(p => p.Id == postId);
       
        if (post == null) {
            return "post not found";
        }

        post.Upvotes++;
        
        db.SaveChanges();
        return "Vote updated"; 
    }
    public string UpdateDownvotePost(int postId)
    {
        Post? post = db.Posts.FirstOrDefault(p => p.Id == postId);


        if (post == null)
        {
            return "post not found";
        }

        post.Downvotes--;

        db.SaveChanges();
        return "Vote updated";
    }

    public string UpdateUpvoteComment(int postId, int commentId)
    {
        Post? post = db.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == postId);
        if (post == null) return "post not found";

        Comment? comment = post.Comments.FirstOrDefault(t => t.Id == commentId);
        if (comment == null) return "Comment not found";

        comment.Upvotes++;
        
        db.SaveChanges();
        return "comment updated"; 
    }
    public string UpdateDownvoteComment(int postId, int commentId)
    {
        Post? post = db.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == postId);
        if (post == null) return "post not found";

        Comment? comment = post.Comments.FirstOrDefault(t => t.Id == commentId);
        if (comment == null) return "Comment not found";

        comment.Downvotes--;

        db.SaveChanges();
        return "Comment updated";
    }
    public User CreateUser(int userId) 
    {
        User newUser = new User() { Id = userId, Username = "" };
        db.Users.Add(newUser);
        return new User(){Id = userId, Username = "" };
        ;
    }

    public string CreatePost(int userId, string title, PostType type, string content) {
        User? user = db.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) user = CreateUser(userId);
        if (user == null) return "user not found";
        Post newPost = new Post
        {
            Created = DateTime.Now,
            UserId = userId,
            Author = user,
            Title = title,
            Type = type,
            Content = content
        };
        db.Posts.Add(newPost);
        db.SaveChanges();
        return "Post created";
    }
    public Comment? CreateComment(int postId, int userId, string content) {
        User? user = db.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) user = CreateUser(userId);
        if (user == null)
        {
            Console.WriteLine("user not found");
            return null;
        }
        Comment newComment = new Comment
        {
            Content = content,
            UserId = userId,
            Author = user,
            Created = DateTime.Now //have to cehck latter
        };
        Post? post = db.Posts.FirstOrDefault(p => p.Id == postId);
        if (post == null) {
            Console.WriteLine("post not found");
            return null;
        }
        post.Comments.Add(newComment);
        db.SaveChanges();
        
        return newComment;
    }
}
