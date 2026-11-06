using FecebookClass;
using FecebookClass.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookApp.services
{
   
        public class GroupService
        {
            private readonly FacebookDbContext _context;

            public GroupService(FacebookDbContext context)
            {
                _context = context;
            }

            // Get All Groups
           
            public List<Group> GetAllGroups()
            {
                return _context.Groups
                    .Include(g => g.groupMembers)
                    .ToList();
            }

            // Create Group
          
            public Group CreateGroup(int userId, string name)
            {
                Group group = new Group
                {
                    Name = name
                };

                _context.Groups.Add(group);
                _context.SaveChanges();

                GroupMember member = new GroupMember
                {
                    UserId = userId,
                    GroupId = group.Id
                };

                _context.GroupMembers.Add(member);
                _context.SaveChanges();

                return group;
            }

            // Join Group
            public void JoinGroup(int userId, int groupId)
            {
                var group = _context.Groups
                    .FirstOrDefault(g => g.Id == groupId);

                if (group == null)
                    throw new Exception("Group not found.");

                var alreadyMember = _context.GroupMembers.Any(gm =>
                    gm.UserId == userId &&
                    gm.GroupId == groupId);

                if (alreadyMember)
                    throw new Exception("User already joined this group.");

                GroupMember member = new GroupMember
                {
                    UserId = userId,
                    GroupId = groupId
                };

                _context.GroupMembers.Add(member);
                _context.SaveChanges();
            }

            // Leave Group
            public void LeaveGroup(int userId, int groupId)
            {
                var member = _context.GroupMembers
                    .FirstOrDefault(gm =>
                        gm.UserId == userId &&
                        gm.GroupId == groupId);

                if (member == null)
                    throw new Exception("User is not a member.");

                _context.GroupMembers.Remove(member);
                _context.SaveChanges();
            }

            // Delete Group
            public void DeleteGroup(int groupId, int adminId)
            {
                var group = _context.Groups
                    .Include(g => g.groupMembers)
                    .FirstOrDefault(g => g.Id == groupId);

                if (group == null)
                    throw new Exception("Group not found.");

              
                _context.GroupMembers.RemoveRange(group.groupMembers);

                _context.Groups.Remove(group);

                _context.SaveChanges();
            }
        }
    }
