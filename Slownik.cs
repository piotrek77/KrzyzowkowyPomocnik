using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrzyzowkowyPomocnik
{
    internal class Slownik
    {

        private static Slownik _instance;

        public static Slownik GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Slownik();
            }
            return _instance;
        }



        List<string> slowa = new List<string>();

        public List<string> Slowa { get { return slowa; } }

        string connectionString = "DataSource=sjp.db";
        private Slownik()
        {
            slowa.Clear();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT slowo FROM slowa";
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            string slowo = reader.GetString(0);
                            slowa.Add(slowo);
                        }
                    }
                }
            }
        }
    }
}
