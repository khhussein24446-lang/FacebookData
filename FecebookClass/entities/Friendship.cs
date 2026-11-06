using FecebookClass.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookClass.entities
{
    public class Friendship : BaseEntity
    {
        public int Id { get; set;}

        public status statues { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
