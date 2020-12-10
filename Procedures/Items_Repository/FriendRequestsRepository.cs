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
    public class FriendRequestsRepository : Repository, IRepsitory<Solicitudes_Amistad>
    {
        public bool Add(Solicitudes_Amistad item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO REQUEST (ID_USER_REQUEST, ID_USER_REQUESTED, DATE_REQUEST) "
                                               + "VALUES(@user, @friend, '@date')");
            command.Parameters.AddWithValue("@user", item.ID_User);
            command.Parameters.AddWithValue("@friend", item.ID_Friend);
            command.Parameters.AddWithValue("@date", item.Request_Date);

            return ExecuteDml(command);
        }
        public bool DeleteRequest(int id_user, int id_friend)
        {

            SqlCommand command = new SqlCommand("DELETE REQUEST WHERE ID_USER_REQUEST = @ID AND ID_USER_REQUESTED = @ID_FRIEND");
            command.Parameters.AddWithValue("@ID", id_user);
            command.Parameters.AddWithValue("@ID_FRIEND", id_friend);

            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Solicitudes_Amistad item)
        {
            throw new NotImplementedException();
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
