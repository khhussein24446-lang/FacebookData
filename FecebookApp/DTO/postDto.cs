using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookApp.DTO
{
    internal class postDto
    {
        public int Id { get; set; }
        public string text { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreateAt { get; set; }
        public int LikesCount { get; set; }
    }
}
