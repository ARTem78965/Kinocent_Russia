using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Авторизация
{
    class DBUtils
    {
        public static MySqlConnection DBConnection()
        {
            {
                string host = "127.0.0.1";
                int port = 3306;
                string database = "CenterCinema";
                string username = "artem";
                string password = "A331166a";

                return DataBase.GetDBConnection(host, port, database, username, password);
            }
        }

        internal class GetDBConnection
        {
            public GetDBConnection()
            {
            }
        }
    }
}
