using FecebookClass;
using FecebookClass.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookApp.services
{
    public class CommentService
    {
            private readonly FacebookDbContext _context;

            public CommentService(FacebookDbContext context)
            {
                _context = context;
            }

            // Comment on Post
            public comment CommentOnPost(
                int postId,
                int userId,
                string message)
            {
                var post = _context.posts
                    .FirstOrDefault(p => p.Id == postId);

                if (post == null)
                    throw new Exception("Post not found.");

                comment newComment = new comment
                {
                    PostId = postId,
                    UserId = userId,
                    Message = message
                };

                _context.Comments.Add(newComment);
                _context.SaveChanges();

                return newComment;
            }

            // Reply to Comment
            public comment ReplyToComment(
                int commentId,
                int userId,
                string message)
            {
                var parentComment = _context.Comments
                    .FirstOrDefault(c => c.Id == commentId);

                if (parentComment == null)
                    throw new Exception("Comment not found.");

                comment reply = new comment
                {
                    PostId = parentComment.PostId,
                    UserId = userId,
                    Message = message,
                    ParentComId = commentId
                };

                _context.Comments.Add(reply);
                _context.SaveChanges();

                return reply;
            }
        }
    }

