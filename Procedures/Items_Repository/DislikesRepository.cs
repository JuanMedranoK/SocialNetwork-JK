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
    public class DislikesRepository : Repository, IRepsitory<Dislikes>
    {
        public bool Add(Dislikes item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO DISLIKES (ID_POST_DISLIKE, ID_USER_DISLIKE) "
                                               + "VALUES(@post, @user)");
            command.Parameters.AddWithValue("@post", item.ID_POST);
            command.Parameters.AddWithValue("@user", item.ID_USER);

            return ExecuteDml(command);
        }

        public bool DeleteLike(Dislikes item)
        {
            SqlCommand command = new SqlCommand("DELETE FROM DISLIKES WHERE ID_USER_DISLIKE = @user AND ID_POST_DISLIKE = @post");

            command.Parameters.AddWithValue("@post", item.ID_POST);
            command.Parameters.AddWithValue("@user", item.ID_USER);

            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Dislikes item)
        {
            throw new NotImplementedException();
        }

        public DataTable Get(string query)
        {
            throw new NotImplementedException();
        }
    }
}
