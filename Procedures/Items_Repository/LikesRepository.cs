using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Entities;

namespace Procedures.Items_Repository
{
    public class LikesRepository : Repository, IRepsitory<Likes>
    {
        public bool Add(Likes item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO LIKES (ID_POST_LIKE, ID_USER_LIKE) "
                                               + "VALUES(@post, @user)");
            command.Parameters.AddWithValue("@post", item.ID_POST);
            command.Parameters.AddWithValue("@user", item.ID_USER);

            return ExecuteDml(command);
        }

        public bool DeleteLike(Likes item)
        {
            SqlCommand command = new SqlCommand("DELETE FROM LIKES WHERE ID_USER_LIKE = @user AND ID_POST_LIKE = @post");

            command.Parameters.AddWithValue("@post", item.ID_POST);
            command.Parameters.AddWithValue("@user", item.ID_USER);

            return ExecuteDml(command);
        }

        public bool Edit(Likes item)
        {
            throw new NotImplementedException();
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
