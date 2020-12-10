using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Publicaciones
    {
        public int ID_Post{ get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
    }
}
