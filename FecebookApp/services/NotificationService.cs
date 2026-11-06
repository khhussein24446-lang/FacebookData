using FecebookClass;
using FecebookClass.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookApp.services
{
    public class NotificationService
    {
            private readonly FacebookDbContext _context;

            public NotificationService(FacebookDbContext context)
            {
                _context = context;
            }

            public List<Notification> GetNotifications(int userId)
            {
                return _context.Notifications
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedAt)
                    .ToList();
            }

            public void CreateNotification(
                int userId,
                string message)
            {
                Notification notification = new Notification
                {
                    UserId = userId,
                    Message = message
                };

                _context.Notifications.Add(notification);
                _context.SaveChanges();
            }
        }
    }

