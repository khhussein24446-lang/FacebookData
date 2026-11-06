using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookClass.entities
{
    public class Group : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupMember> groupMembers { get; set; } = new List<GroupMember>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
