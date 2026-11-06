using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookClass.entities
{
    public class User: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        public string Bio {  get; set; }
        public DateTime Date {  get; set; }
        public int LiekId { get; set; }

        public int UserProfilId { get; set; }
         public UserProfile userProfile { get; set; }
        public ICollection<Post> createPosts { get; set; }=new List<Post>();
        public ICollection<Like> likes { get; set; } = new List<Like>();
        public ICollection<comment> comments { get; set; } = new List<comment>();
        public ICollection<GroupMember> groupMembers { get; set; } = new List<GroupMember>();
        public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();


    }
}
