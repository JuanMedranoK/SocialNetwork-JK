using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedures.Items_Repository
{
    public class FriendsRepository : Repository, IRepsitory<Amigos>
    {
        public bool Add(Amigos item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO FRIENDS (ID_USER, ID_FRIEND, FRIENDSHIP_DATE) "
                                               + "VALUES(@user, @friend, @date)");
            command.Parameters.AddWithValue("@user", item.ID_User);
            command.Parameters.AddWithValue("@friend", item.ID_Friend);
            command.Parameters.AddWithValue("@date", item.Friendship_Date);

            return ExecuteDml(command);
        }
        public bool DeleteFriendship(int id_user, int id_friend)
        {

            SqlCommand command = new SqlCommand("DELETE FRIENDS WHERE ID_USER = @ID AND ID_FRIEND = @ID_FRIEND");
            command.Parameters.AddWithValue("@ID", id_user);
            command.Parameters.AddWithValue("@ID_FRIEND", id_friend);

            return ExecuteDml(command);
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Amigos item)
        {
            throw new NotImplementedException();
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
