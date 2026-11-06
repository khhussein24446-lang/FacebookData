using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookClass.entities
{
    public class Post : BaseEntity
    {
        public int Id { get; set; }
        [MinLength(5)]
        [MaxLength(100)]
        public string text { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public ICollection<Like> likes { get; set; } = new List<Like>();
        public ICollection<comment> comments { get; set; } = new List<comment>();

    }
}
