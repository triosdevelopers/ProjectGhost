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
        public bool isAddUser { get; set; }
        public string cs = "Filename=Ghost.db";


        public void CheckID(string email, string password)
        {
            UserID = 0;
            GhostID = 0;

            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string sql = "SELECT us.userID, us.username, " +
                    "us.password FROM User AS us WHERE us.username='" + email + "'" +
                    "AND us.password='" + Security.HashSHA1(password) + "'";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                   
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        UserID = Convert.ToInt32(result);
                    }
                }
                con.Close();
            }
            
            //================================================ GETS THE GHOST ID ON LOGIN

            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string sql = "SELECT us.GhostID FROM User as us WHERE us.Username='" + 
                    email + "' AND us.Password='" + Security.HashSHA1(password) + "'";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    var ghost = cmd.ExecuteScalar();
                    if (ghost != null)
                    {
                        GhostID = Convert.ToInt32(ghost);
                    }
                }
                con.Close();
            }
        }


        public void CreateAccount(string email, string password, string serialNum, string ghostName)
        {
            //=================================================== gets the ghost type

            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string sql = "SELECT GhostTypeID FROM GhostType WHERE Name='" + ghostName + "'";
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

                string sql = "INSERT INTO Ghost(SerialNumber, GhostTypeID) VALUES " +
                    "('" + serialNum + "','" + GhostTypeID + "')";

                string sql2 = " SELECT gh.GhostID FROM ghost AS gh " +
                "WHERE gh.SerialNumber ='" + serialNum + "'";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    var insert = cmd.ExecuteScalar();

                    using (SqliteCommand cmd2 = new SqliteCommand(sql2, con))
                    {

                        var newGhost = cmd2.ExecuteScalar();
                        if (newGhost != null)
                        {
                            GhostID = Convert.ToInt32(newGhost);
                        }

                    }

                }
                con.Close();

                
            }


            //=================================================== adds new user

            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();

                string sql = "INSERT INTO user (Username, Password, GhostID) " +
                "VALUES ('" + email + "','" + Security.HashSHA1(password) + "','" + GhostID + "') ";

                string sql2 ="SELECT* FROM user AS us WHERE us.Username = '" + email + "'";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    var insert = cmd.ExecuteNonQuery();

                   
                }
                

                using (SqliteCommand cmd2 = new SqliteCommand(sql2, con))
                {
                    var newUser = cmd2.ExecuteScalar();
                    if (newUser != null)
                    {
                        UserID = Convert.ToInt32(newUser);
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

                string sql = "SELECT gt.Name FROM GhostType as gt";
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

        public void AddOptions(int brit, int contr, int vol, bool led, bool mic, bool prox, bool audio)
        {
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                int NewOptionsID = 0;
                con.Open();
                string sql = "INSERT INTO GhostProtocol " +
                    "(CameraBrightness, CameraContrast, Volume, LedState, MicState, " +
                    "MotionSensorState, SpeakerState, GhostID) VALUES(" +
                   "'" + brit +"','"+ contr + "','" + led + "','" + vol + "','" + mic + "','" + 
                    prox + "','" + audio + "','" + GhostID + "')";
                string sql2 = "SELECT * FROM GhostProtocol LAST_INSERT_ROWID ";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    var options = cmd.ExecuteScalar();
                }
                using (SqliteCommand cmd2 = new SqliteCommand(sql2, con))
                {
                    var optionsID = cmd2.ExecuteScalar();
                    if (optionsID != null)
                    {
                        NewOptionsID = Convert.ToInt32(optionsID);
                    }
                }
                con.Close();
            }
        }

        // ANDREWS CAMERA SCHEDULE QUERY THING //
        public void updateSchedule(int day, int on, int off, int capType, int recDur, int recDel, int snapDel)
        {
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                int cState = 1;
                int SnapshotCount = 5;
                int NewScheduleID = 0;
                con.Open();
                string sql = "INSERT INTO CameraSchedule " +
                    "(DayOfWeek, OnTime, Offtime, CameraState, CaptureType, RecordingDuration, RecordingDelay, SnapshotCount, SnapshotDelay) Values(" +
                    "'" + day + "','" + on + "','" + off + "','" + cState + "','" + capType + "','" + recDur + "','" + recDel + "','" + SnapshotCount + "','" + snapDel + "')";
                string sql2 = "SELECT * FROM CameraSchedule LAST_INSERT_ROWID";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    var sched = cmd.ExecuteScalar();
                }
                using (SqliteCommand cmd2 = new SqliteCommand(sql2, con))
                {
                    var schedID = cmd2.ExecuteScalar();
                    if (schedID != null)
                    {
                        NewScheduleID = Convert.ToInt32(schedID);
                    }
                }
                con.Close();
            }
        }
    }
}
