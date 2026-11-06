using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookApp.DTO
{
   public class Comment
    {
        public int Id { get; set; } 
        public string Message  { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int? parentCommentId { get; set; }

    }
}
