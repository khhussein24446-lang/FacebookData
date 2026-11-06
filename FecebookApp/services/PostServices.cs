using FecebookClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FecebookClass.entities;


namespace FecebookApp.services
{
   
        public class PostService
        {
            private readonly FacebookDbContext db;
            public PostService(FacebookDbContext db)
            {
                this.db = db;
            }
            // 1. Create Post
            public void CreatePost(int userId, string text)
            { try {
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Console.WriteLine("Post cannot be empty.");
                        return;
                    }
                    int wordCount = text
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Length;
                    if (wordCount <= 5 || wordCount >= 100)
                    {
                        Console.WriteLine(
                            "Post must contain more than 5 words and fewer than 100 words."
                        );
                        return;
                    }
                    Post post = new Post
                    {
                        text = text,
                        UserId = userId
                    };
                    db.posts.Add(post);
                    db.SaveChanges();
                    Console.WriteLine("Post created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while creating post: {ex.Message}");
                }}
            // 2. Update Post
            public void UpdatePost(int postId, int userId, string newText)
            {
                try
                {
                    Post post = db.posts
                        .FirstOrDefault(p => p.Id == postId);

                    if (post == null)
                    {
                        Console.WriteLine("Post not found.");
                        return;
                    }

                    if (post.UserId != userId)
                    {
                        Console.WriteLine("You can only update your own post.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(newText))
                    {
                        Console.WriteLine("Post cannot be empty.");
                        return;
                    }

                    int wordCount = newText
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Length;

                    if (wordCount <= 5 || wordCount >= 100)
                    {
                        Console.WriteLine(
                            "Post must contain more than 5 words and fewer than 100 words."
                        );
                        return;
                    }

                    post.text = newText;

                    db.SaveChanges();

                    Console.WriteLine("Post updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating post: {ex.Message}");
                }
            }
            //  Delete Post
            public void DeletePost(int postId, int userId)
            {
                try
                {
                    Post post = db.posts
                        .FirstOrDefault(p => p.Id == postId);

                    if (post == null)
                    {
                        Console.WriteLine("Post not found.");
                        return;
                    }

                    if (post.UserId != userId)
                    {
                        Console.WriteLine("You can only delete your own post.");
                        return;
                    }

                    db.posts.Remove(post);
                    db.SaveChanges();

                    Console.WriteLine("Post deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deleting post: {ex.Message}");
                }
            }
            // 4. Like Post
            public void LikePost(int postId, int userId)
            {
                try
                {
                    Post post = db.posts
                        .FirstOrDefault(p => p.Id == postId);

                    if (post == null)
                    {
                        Console.WriteLine("Post not found.");
                        return;
                    }

                    bool alreadyLiked = db.Likes
                        .Any(l => l.PostId == postId &&
                                  l.UserId == userId);

                    if (alreadyLiked)
                    {
                        Console.WriteLine("You already liked this post.");
                        return;
                    }

                    Like like = new Like
                    {
                        PostId = postId,
                        UserId = userId
                    };

                    db.Likes.Add(like);
                    db.SaveChanges();

                    Console.WriteLine("Post liked successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while liking post: {ex.Message}");
                }
            }


            // 5. Comment On Post
            public void CommentOnPost(
                int postId,
                int userId,
                string text)
            {
                try
                {
                    Post post = db.posts
                        .FirstOrDefault(p => p.Id == postId);

                    if (post == null)
                    {
                        Console.WriteLine("Post not found.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Console.WriteLine("Comment cannot be empty.");
                        return;
                    }

                    comment newComment = new comment
                    {
                        PostId = postId,
                        UserId = userId,
                        Message = text
                    };

                    db.Comments.Add(newComment);
                    db.SaveChanges();

                    Console.WriteLine("Comment added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while adding comment: {ex.Message}");
                }
            }


            // 6. Reply To Comment
            public void ReplyToComment(
                int commentId,
                int userId,
                string text)
            {
                try
                {
                    comment parentComment = db.Comments
                        .FirstOrDefault(c => c.Id == commentId);

                    if (parentComment == null)
                    {
                        Console.WriteLine("Comment not found.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Console.WriteLine("Reply cannot be empty.");
                        return;
                    }

                    comment reply = new comment
                    {
                        PostId = parentComment.PostId,
                        UserId = userId,
                        Message = text,
                        ParentComId = commentId
                    };

                    db.Comments.Add(reply);
                    db.SaveChanges();

                    Console.WriteLine("Reply added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while replying: {ex.Message}");
                }
            }
            // 7. Get Post Details
            public void GetPostDetails(int postId)
            {
                try
                {
                    Post post = db.posts
                        .Include(p => p.user)
                        .Include(p => p.likes)
                        .Include(p => p.comments)
                        .FirstOrDefault(p => p.Id == postId);

                    if (post == null)
                    {
                        Console.WriteLine("Post not found.");
                        return;
                    }
                    Console.WriteLine("\n=========================");
                    Console.WriteLine("       POST DETAILS");
                    Console.WriteLine("=========================");

                    Console.WriteLine($"Post ID: {post.Id}");
                    Console.WriteLine($"User ID: {post.UserId}");
                    Console.WriteLine($"Text: {post.text}");

                    Console.WriteLine(
                        $"Number of Likes: {post.likes.Count}"
                    );

                    Console.WriteLine("\nLikes:");

                    foreach (var like in post.likes)
                    {
                        Console.WriteLine(
                            $"User ID: {like.UserId}"
                        );
                    }

                    Console.WriteLine("\nComments:");

                    foreach (var c in post.comments)
                    {
                        Console.WriteLine(
                            $"User {c.UserId}: {c.Message}"
                        );
                    }

                    Console.WriteLine("=========================");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Error while getting post details: {ex.Message}"
                    );
                }
            }
            // 8. Search Posts
            public void SearchPosts(string keyword)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        Console.WriteLine("Please enter a keyword.");
                        return;
                    }

                    var posts = db.posts
                        .Where(p => p.text.Contains(keyword))
                        .OrderByDescending(p => p.Id)
                        .ToList();

                    if (posts.Count == 0)
                    {
                        Console.WriteLine("No posts found.");
                        return;
                    }
                    Console.WriteLine("\n=========================");
                    Console.WriteLine("      SEARCH RESULTS");
                    Console.WriteLine("=========================");
                    foreach (var post in posts)
                    {
                        Console.WriteLine($"Post ID: {post.Id}");
                        Console.WriteLine($"User ID: {post.UserId}");
                        Console.WriteLine($"Text: {post.text}");
                        Console.WriteLine("-------------------------");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Error while searching posts: {ex.Message}"
                    );
                }
            }
        }
}
