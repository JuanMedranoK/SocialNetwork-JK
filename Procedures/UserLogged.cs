using Database.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedures
{
    public sealed class UserLogged
    {
        public Usuarios user { get; set; } = null;

        public static UserLogged Instance { get; } = new UserLogged();

        public void logIn(DataTable dt)
        {
            Usuarios user = new Usuarios();
            user.ID_USER = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
            user.Name = dt.Rows[0].ItemArray[1].ToString();
            user.Lastname = dt.Rows[0].ItemArray[2].ToString();
            user.Mail = dt.Rows[0].ItemArray[3].ToString();
            user.Password = dt.Rows[0].ItemArray[4].ToString();
            user.ProfilePicture = dt.Rows[0].ItemArray[5].ToString();
            user.Portrait = dt.Rows[0].ItemArray[6].ToString();
            user.UserName = dt.Rows[0].ItemArray[7].ToString();
            this.user = user;
        }
        public void logOut()
        {
            Usuarios user = new Usuarios();
            user.ID_USER = 0;
            user.Name = "";
            user.Lastname = "";
            user.Mail = "";
            user.ProfilePicture = "";
            user.Portrait = "";
            user.Password = "";
            this.user = user;
        }
    }
}
