using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Comentarios
    {
        public int ID_Comment { get; set; }
        public int ID_Post { get; set; }
        public int ID_User { get; set; }
        public int Type_Comment { get; set; }
        public int Comment_Answered { get; set; }
        public string Comment { get; set; }
        public string TimeDate { get; set; }
        public string User_Answered { get; set; }

    }
}
