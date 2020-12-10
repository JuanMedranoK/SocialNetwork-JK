using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Entities;
using System.Data.SqlClient;


namespace Procedures.Items_Repository
{
    public class UsersRepository : Repository, IRepsitory<Database.Entities.Usuarios>
    {
        public bool Add(Database.Entities.Usuarios item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO USERS (NAME_USER, LASTNAME_USER, MAIL_USER, PASSWORD_USER, PROFILE_PICTURE_USER, PORTRAIT_USER,USERNAME) "
                                               + "VALUES(@name, @lastname, @mail, @password, @pp, @portrait,@username)");
            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@lastname", item.Lastname);
            command.Parameters.AddWithValue("@mail", item.Mail);
            command.Parameters.AddWithValue("@password", item.Password);
            command.Parameters.AddWithValue("@pp", item.ProfilePicture);
            command.Parameters.AddWithValue("@portrait", item.Portrait);
            command.Parameters.AddWithValue("@username", item.UserName);

            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Database.Entities.Usuarios item)
        {
            SqlCommand command = new SqlCommand("UPDATE USERS SET NAME_USER = @name, LASTNAME_USER = @lastname, MAIL_USER = @mail, PASSWORD_USER = @password,"
                + " PROFILE_PICTURE_USER = @pp, PORTRAIT_USER =@portrait WHERE ID_USER = @ID");
            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@lastname", item.Lastname);
            command.Parameters.AddWithValue("@mail", item.Mail);
            command.Parameters.AddWithValue("@password", item.Password);
            command.Parameters.AddWithValue("@pp", item.ProfilePicture);
            command.Parameters.AddWithValue("@portrait", item.Portrait);
            command.Parameters.AddWithValue("@ID", item.ID_USER);

            return ExecuteDml(command);
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
