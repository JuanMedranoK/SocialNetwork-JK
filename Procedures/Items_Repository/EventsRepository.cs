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
    public class EventsRepository : Repository, IRepsitory<Eventos>
    {
        public bool Add(Eventos item)
        {
            SqlCommand command = new SqlCommand("INSERT INTO EVENT(ID_HOST_USER, NAME_EVENT, DESCRIPTION_EVENT, DATE_EVENT, ADDRESS) "
                                                  + "VALUES(@idUser, @name, @description, @date, @address)");
            command.Parameters.AddWithValue("@idUser", item.ID_HOST);
            command.Parameters.AddWithValue("@name", item.NAME_EVENT);
            command.Parameters.AddWithValue("@description", item.DESCRIPTION_EVENT);
            command.Parameters.AddWithValue("@date", item.DATE_EVENT);
            command.Parameters.AddWithValue("@address", item.ADDRESS_EVENT);

            return ExecuteDml(command);
        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM EVENT WHERE ID_EVENT = @ID");
            command.Parameters.AddWithValue("ID", id);
            return ExecuteDml(command);
        }

        public bool Edit(Eventos item)
        {
            SqlCommand command = new SqlCommand("UPDATE EVENT SET ID_HOST_USER = @idUser, NAME_EVENT = @name, DESCRIPTION_EVENT = @description, "
                                                  + "DATE_EVENT =  @date, ADDRESS = @address WHERE ID_EVENT = @ID");
            command.Parameters.AddWithValue("@idUser", item.ID_HOST);
            command.Parameters.AddWithValue("@name", item.NAME_EVENT);
            command.Parameters.AddWithValue("@description", item.DESCRIPTION_EVENT);
            command.Parameters.AddWithValue("@date", item.DATE_EVENT);
            command.Parameters.AddWithValue("@address", item.ADDRESS_EVENT);
            command.Parameters.AddWithValue("@ID", item.ID_EVENT);

            return ExecuteDml(command);
        }

        public DataTable Get(string query)
        {
            return ExecuteRead(query);
        }
    }
}
