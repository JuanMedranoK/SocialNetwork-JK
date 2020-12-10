using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Database
{
    public interface IRepsitory<T>
    {
        bool Add(T item);
        bool Edit(T item);
        bool Delete(int id);
        DataTable Get(string query);
    }
}
