using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;  //to download the file
using System.Data.SqlClient;  //to run the INSERT statement
using System.Configuration; //to read the connection string from web.config
using System.IO;  //to open the downloaded file
using System.Windows.Forms;
using System.Data;
using TravianBot.Library;

namespace TravianBot.Functions
{
    class SqlMap
    {
        private string exception;

        public string getTravianMap(string server)
        {
            try
            {
                //get the directory of the web app
                string mapFile = Application.StartupPath + @"\Data\" + "\\map.sql";
                WebClient client = new WebClient();

                //download the file to the specified location, using HTTP
                client.DownloadFile("http://" + server + "/map.sql", mapFile);

                string connectionString = "Server=LEORIC-PC\\SQLEXPRESS; Database=Travian; User Id=Travian; Password = long;";

                SqlConnection aspNetDb = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aspNetDb;
                aspNetDb.Open();

                StreamReader sr = new StreamReader(mapFile);

                string line;

                //loop thru the lines in the file
                while ((line = sr.ReadLine()) != null)
                {
                    //Remove the back tick from the name of the table, because this only
                    //works with MySQL, in SQL Server this throws an error
                    line = line.Replace("`", "");

                    cmd.CommandText = line;
                    cmd.ExecuteNonQuery();
                }
                aspNetDb.Close();
                exception = "";
            }
            catch
            {
                exception = "mapError";
            }

            return exception;
        }

        public void deleteData()
        {
            string connectionString = "Server=LEORIC-PC\\SQLEXPRESS; Database=Travian; User Id=Travian; Password = long;";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("DELETE FROM x_world", conn);
            command.ExecuteNonQuery();

            conn.Close();
        }

        public static Coordinates getCoordinatefromVillageID(int id)
        {
            int x = 0;
            int y = 0;
            string connectionString = "Server=LEORIC-PC\\SQLEXPRESS; Database=Travian; User Id=Travian; Password = long;";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT x, y FROM [x_world] WHERE vid=@zip", conn);
            command.Parameters.AddWithValue("@zip", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                { 
                    x = Convert.ToInt32(reader[0]);
                    y = Convert.ToInt32(reader[1]);
                }
            }
            Coordinates xy = new Coordinates(x, y);
                        
            conn.Close();
            return xy;
        }

        public static int getTileIDfromVillageID(int id)
        {
            int tid = 0;
            string connectionString = "Server=LEORIC-PC\\SQLEXPRESS; Database=Travian; User Id=Travian; Password = long;";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT id FROM [x_world] WHERE vid=@zip", conn);
            command.Parameters.AddWithValue("@zip", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    tid = Convert.ToInt32(reader[0]);
                }
            }

            conn.Close();
            return tid;
        }

        public static string getNanefromTileID(int id)
        {
            string name = "";
            string connectionString = "Server=LEORIC-PC\\SQLEXPRESS; Database=Travian; User Id=Travian; Password = long;";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT village FROM [x_world] WHERE id=@zip", conn);
            command.Parameters.AddWithValue("@zip", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    name = Convert.ToString(reader[0]);
                }
            }
            conn.Close();
            return name;
        }

        public static Coordinates getCoordinatefromTileID(int id)
        {
            int x = 0;
            int y = 0;
            string connectionString = "Server=LEORIC-PC\\SQLEXPRESS; Database=Travian; User Id=Travian; Password = long;";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT x, y FROM [x_world] WHERE id=@zip", conn);
            command.Parameters.AddWithValue("@zip", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    x = Convert.ToInt32(reader[0]);
                    y = Convert.ToInt32(reader[1]);
                }
            }
            Coordinates xy = new Coordinates(x, y);

            conn.Close();
            return xy;
        }
    }
}
