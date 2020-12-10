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
    public class CommentsRepository : Repository, IRepsitory<Comentarios>
    {
        public bool Add(Comentarios item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO COMMENTS (ID_POST_COMMENT, ID_USER_COMMENT, COMMENT_TYPE, ID_COMMENT_ANSWERED,NAME_USER_ANSWERED, COMMENT, TIMEDATE) "
                                               + "VALUES(@ID_POST, @ID_USER, @TYPE, @ID_COMMENT_A,@NAME_U, @COMMENT, @TIMEDATE)");
            command.Parameters.AddWithValue("@ID_POST", item.ID_Post);
            command.Parameters.AddWithValue("@ID_USER", item.ID_User);
            command.Parameters.AddWithValue("@TYPE", item.Type_Comment);
            command.Parameters.AddWithValue("@ID_COMMENT_A", item.Comment_Answered);
            command.Parameters.AddWithValue("@NAME_U", item.User_Answered);
            command.Parameters.AddWithValue("@COMMENT", item.Comment);
            command.Parameters.AddWithValue("@TIMEDATE", item.TimeDate);

            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE COMMENTS WHERE ID_COMMENT = @ID");
            command.Parameters.AddWithValue("@ID", id);

            return ExecuteDml(command);
        }

        public bool Edit(Comentarios item)
        {
            SqlCommand command = new SqlCommand("UPDATE COMMENTS SET COMMENT = @COMMENT"
                + " WHERE ID_COMMENT = @ID");
            command.Parameters.AddWithValue("@COMMENT", item.Comment);
            command.Parameters.AddWithValue("@ID", item.ID_Comment);
            return ExecuteDml(command);
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
