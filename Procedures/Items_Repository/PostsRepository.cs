using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Procedures.Items_Repository
{
    public class PostsRepository : Repository, IRepsitory<Publicaciones>
    {
        public bool Add(Publicaciones item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO POST (ROUTE_POST, ID_USER_POST, DATE_POST, DESCRIPTION_POST) "
                                               + "VALUES(@route, @id, @date, @caption)");
            command.Parameters.AddWithValue("@route", item.Route);
            command.Parameters.AddWithValue("@id", item.UserId);
            command.Parameters.AddWithValue("@date", item.Date);
            command.Parameters.AddWithValue("@caption", item.Description);

            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE POST WHERE ID_POST = @ID");
            command.Parameters.AddWithValue("@ID", id);

            return ExecuteDml(command);
        }

        public bool Edit(Publicaciones item)
        {
            SqlCommand command = new SqlCommand("UPDATE POST SET DESCRIPTION_POST = @caption WHERE ID_POST = @id");
            command.Parameters.AddWithValue("@caption", item.Description);
            command.Parameters.AddWithValue("@id", item.ID_Post);

            return ExecuteDml(command);
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
