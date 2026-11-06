using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookApp.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
