using FecebookClass;
using FecebookClass.entities;
using FecebookClass.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FecebookApp.services
{
    public class FriendService
    {
            private readonly FacebookDbContext _context;
            public FriendService(FacebookDbContext context)
            {
                _context = context;
            }
            // Send Friend Request
            public void SendFriendRequest(int senderId, int receiverId)
            {
                if (senderId == receiverId)
                    throw new Exception("You cannot send a request to yourself.");

                var exists = _context.Friendships.Any(f =>
                    (f.UserId == senderId && f.Id == receiverId) ||
                    (f.UserId == receiverId && f.Id == senderId));

                if (exists)
                    throw new Exception("Friend request already exists.");

                Friendship friendship = new Friendship
                {
                    UserId = senderId,
                    Id = receiverId,
                    statues = status.Pending
                };

                _context.Friendships.Add(friendship);
                _context.SaveChanges();
            }
            // Accept
            public void AcceptFriendRequest(int friendshipId)
            {
                var friendship = _context.Friendships
                    .FirstOrDefault(f => f.Id == friendshipId);

                if (friendship == null)
                    throw new Exception("Friend request not found.");

                friendship.statues = status.Accepted;
            
                _context.SaveChanges();
            }

            // Reject
            public void RejectFriendRequest(int friendshipId)
            {
                var friendship = _context.Friendships
                    .FirstOrDefault(f => f.Id == friendshipId);

                if (friendship == null)
                    throw new Exception("Friend request not found.");

                friendship.statues = status.Rejected;

                _context.SaveChanges();
            }

            // Remove Friend
            public void RemoveFriend(int friendshipId)
            {
                var friendship = _context.Friendships
                    .FirstOrDefault(f => f.Id == friendshipId);

                if (friendship == null)
                    throw new Exception("Friendship not found.");

                _context.Friendships.Remove(friendship);
                _context.SaveChanges();
            }

            // Get Friends
            public List<User> GetFriends(int userId)
            {
                var friendships = _context.Friendships
                    .Where(f =>
                        f.statues == status.Accepted &&
                        (f.UserId == userId || f.Id == userId))
                    .ToList();

                var friendIds = friendships
                    .Select(f =>
                        f.UserId == userId
                            ? f.Id
                            : f.UserId)
                    .ToList();

                return _context.Users
                    .Where(u => friendIds.Contains(u.Id))
                    .ToList();
            }
        }
    }
