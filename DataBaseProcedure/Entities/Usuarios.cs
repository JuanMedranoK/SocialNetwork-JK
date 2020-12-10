using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Usuarios
    {
        public int ID_USER { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string Portrait { get; set; }
        public string UserName { get; set; }
    }
}
