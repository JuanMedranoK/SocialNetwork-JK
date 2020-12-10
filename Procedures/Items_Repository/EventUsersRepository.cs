using Database;
using DataBaseProcedure.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedures.Items_Repository
{
    public class EventUsersRepository : Repository, IRepsitory<UsuariosEventos>
    {
        public bool Add(UsuariosEventos item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO EVENT_USER (ID_EVENT, ID_USER, STATUS_EVENT_USER) "
                                                     + "VALUES(@idEvent, @idUser, @status)");
            command.Parameters.AddWithValue("@idEvent", item.ID_EVENT);
            command.Parameters.AddWithValue("@idUser", item.ID_USER);
            command.Parameters.AddWithValue("@status", item.STATUS);

            return ExecuteDml(command);
        }

        public bool DeleteEventUser(int idEvent, int idUser)
        {
            SqlCommand command = new SqlCommand("DELETE FROM EVENT_USER WHERE ID_EVENT = @idEvent AND ID_USER = @idUser");
            command.Parameters.AddWithValue("idEvent", idEvent);
            command.Parameters.AddWithValue("idUser", idUser);
            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(UsuariosEventos item)
        {
            SqlCommand command = new SqlCommand("UPDATE EVENT_USER SET STATUS_EVENT_USER = @status"
                                                  + " WHERE ID_USER = @idUser AND ID_EVENT = @idEvent");
            command.Parameters.AddWithValue("@idUser", item.ID_USER);
            command.Parameters.AddWithValue("@idEvent", item.ID_EVENT);
            command.Parameters.AddWithValue("@status", item.STATUS);

            return ExecuteDml(command);
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
