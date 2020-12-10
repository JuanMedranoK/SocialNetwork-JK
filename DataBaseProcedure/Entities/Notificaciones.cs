using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Notificaciones
    {
        public int ID_Notification { get; set; }
        public int ID_User { get; set; }
        public int Status { get; set; } //0 read, 1 new
        public int ID_Type { get; set; }
        public string Message { get; set; }
        //TYPES NOTIFICATIONS
        public string Type_Notification { get; set; }
    }
}
