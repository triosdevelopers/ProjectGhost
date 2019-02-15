using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace ProjectGhost
{
    public class Manager
    {
        public List<string> GhostNames = new List<string>();
        public int UserID { get; set; }
        public int GhostTypeID { get; set; }
        public int GhostID { get; set; }
        public string cs = "Data Source=./Ghost.db";

        public void CheckID(string email, string password)
        {
            UserID = 0;
            GhostID = 0;
            using (SqlConnection myConn = new SqlConnection(cs))
            {
                SqlCommand login = new SqlCommand();
                login.Connection = myConn;
                myConn.Open();

                login.Parameters.AddWithValue("@email", email);
                login.Parameters.AddWithValue("@password", Security.HashSHA1(password));

                login.CommandText = ("[spLogin]");
                login.CommandType = System.Data.CommandType.StoredProcedure;

                var result = login.ExecuteScalar();

                if (result != null)
                {
                    UserID = Convert.ToInt32(result);
                }

                myConn.Close();

                //================================================ GETS THE GHOST ID ON LOGIN

                SqlCommand ghostLogin = new SqlCommand();
                ghostLogin.Connection = myConn;
                myConn.Open();

                ghostLogin.Parameters.AddWithValue("@email", email);
                ghostLogin.Parameters.AddWithValue("@password", Security.HashSHA1(password));

                ghostLogin.CommandText = ("[spGhostLogin]");
                ghostLogin.CommandType = System.Data.CommandType.StoredProcedure;

                var ghost = ghostLogin.ExecuteScalar();

                if (ghost != null)
                {
                    GhostID = Convert.ToInt32(result);
                }

                myConn.Close();
            }
        }


        public void CreateAccount(string email, string password, string serialNum, string ghostName)
        {
            //=================================================== gets the ghost type

            using (SqliteConnection con = new SqliteConnection(cs))
            {

                con.Open();
                string sql = "SELECT GhostTypeID FROM ghost_type WHERE Name='" + ghostName + "'";

                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {

                    var ghostType = cmd.ExecuteScalar();

                    if (ghostType != null)
                    {
                        GhostTypeID = Convert.ToInt32(ghostType);
                    }
                }

                con.Close();

            }

            //=================================================== adds new ghost to table

            using (SqliteConnection con = new SqliteConnection(cs))
            {

                con.Open();
                string sql = "INSERT INTO ghost (SerialNumber, GhostTypeID) " +
                "VALUES (" + serialNum + "," + GhostTypeID + ") SELECT* FROM ghost AS gh " +
                "WHERE gh.SerialNumber ='" + serialNum + "'";

                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {

                    var newGhost = cmd.ExecuteScalar();

                    if (newGhost != null)
                    {
                        GhostID = Convert.ToInt32(newGhost);
                    }
                }

                con.Close();

            }

            //=================================================== adds new user

            using (SqliteConnection con = new SqliteConnection(cs))
            {

                con.Open();
                string sql = "INSERT INTO user (Username, Password, GhostID) " +
                "VALUES (" + email + "," + Security.HashSHA1(password) + "," + GhostID + ") " +
                "SELECT* FROM user AS us WHERE us.Username = '" + email + "'";

                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    var newGhost = cmd.ExecuteScalar();

                    if (newGhost != null)
                    {
                        GhostID = Convert.ToInt32(newGhost);
                    }
                }

                con.Close();

            }

        }


        public List<string> ReturnGhostNames()
        {

            using (SqliteConnection con = new SqliteConnection(cs))
            {

                con.Open();
                string sql = "SELECT gt.Name FROM ghost_type as gt";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    SqliteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            GhostNames.Add(Convert.ToString(reader.GetValue(i)));
                        }
                    }
                    reader.Close();
                }

                con.Close();
                return GhostNames;
            }
        }

        public void AddOptions(int brit, int con, bool led, int vol, bool mic, bool prox, bool audio)
        {
            using (SqlConnection myConn = new SqlConnection(cs))
            {

                int NewOptionsID = 0;

                SqlCommand AddOptions = new SqlCommand();
                AddOptions.Connection = myConn;
                myConn.Open();

                AddOptions.Parameters.AddWithValue("@brightness", brit);
                AddOptions.Parameters.AddWithValue("@contrast", con);
                AddOptions.Parameters.AddWithValue("@led", led);
                AddOptions.Parameters.AddWithValue("@volume", vol);
                AddOptions.Parameters.AddWithValue("@mic", mic);
                AddOptions.Parameters.AddWithValue("@proximity", prox);
                AddOptions.Parameters.AddWithValue("@speakers", audio);
                AddOptions.Parameters.AddWithValue("@ghostID", GhostID);
                AddOptions.CommandText = ("[spAddOptions]");
                AddOptions.CommandType = System.Data.CommandType.StoredProcedure;
                var optionsID = AddOptions.ExecuteScalar();

                if (optionsID != null)
                {
                    NewOptionsID = Convert.ToInt32(optionsID);
                }
                myConn.Close();
            }
        }
    }
}
