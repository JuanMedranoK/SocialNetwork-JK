using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Solicitudes_Amistad
    {
        public int ID_Request { get; set; }
        public int ID_User { get; set; }
        public int ID_Friend { get; set; }
        public string Request_Date { get; set; }
    }
}
