using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class ConnenctionString
    {
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        public static string GetConnectionString() {
            server = "localhost";
            database = "killerapp";
            uid = "root";
            password = "root";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            return connectionString;
        }
    }
}
