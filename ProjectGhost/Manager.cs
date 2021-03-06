﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace ProjectGhost
{
    public class Manager
    {
        public List<string> GhostNames = new List<string>();
        public List<string> UserProtocols = new List<string>();
        public List<string> Schedules = new List<string>();
        public int UserID { get; set; }
        public int GhostTypeID { get; set; }
        public int GhostID { get; set; }
        public int OptionsID = -1;
        public int ScheduleID = -1;
        public string cs = "Filename=Ghost.db";

        public int Day;
        public int On;
        public int Off;
        public int capType;
        public int recDur;
        public int recDel;
        public int snapDel;

        public int Brightness;
        public int Contrast;
        public int Volume;
        public int Led;
        public int Microphone;
        public int Proximity;
        public int Audio;
        

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

        public void AddOptions(int brit, int contr, int vol, int led, int mic, int prox, int audio)
        {
            this.Brightness = brit;
            this.Contrast = contr;
            this.Volume = vol;
            this.Led = led;
            this.Microphone = mic;
            this.Proximity = prox;
            this.Audio = audio;

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
                        this.OptionsID = NewOptionsID;
                    }
                }
                con.Close();
            }
        }

        // for saving the schedule to user //
        public void ReturnLatestSchedule()
        {
            Schedules.Clear();
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();

                string sql = "SELECT * FROM CameraSchedule WHERE GhostID ='" + GhostID + "' ORDER BY CameraScheduleID desc LIMIT 1";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    SqliteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Schedules.Add(Convert.ToString(reader.GetValue(i)));
                        }
                    }
                    reader.Close();
                }
                con.Close();
                UpdateSchedules();
            }
        }

        public void UpdateSchedules()
        {
            this.ScheduleID = Convert.ToInt32(Schedules[0]);
            this.Day = Convert.ToInt32(Schedules[1]);
            this.On = Convert.ToInt32(Schedules[2]);
            this.Off = Convert.ToInt32(Schedules[3]);
            this.capType = Convert.ToInt32(Schedules[5]);
            this.recDur = Convert.ToInt32(Schedules[6]);
            this.recDel = Convert.ToInt32(Schedules[7]);
            this.snapDel = Convert.ToInt32(UserProtocols[9]);

        }
        // for saving the schedule to user //
        // for saving the misc options to the user //
        public void ReturnLastOptions()
        {
            UserProtocols.Clear();
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
               
                string sql = "SELECT * FROM GhostProtocol WHERE GhostID ='" + GhostID + "' ORDER BY GhostProtocolsID desc LIMIT 1";
                using (SqliteCommand cmd = new SqliteCommand(sql, con))
                {
                    SqliteDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            UserProtocols.Add(Convert.ToString(reader.GetValue(i)));
                        }
                    }
                    reader.Close();
                }
                con.Close();
                UpdateUserProtocols();
            }
        }

        public void UpdateUserProtocols()
        {
            this.OptionsID = Convert.ToInt32(UserProtocols[0]);
            this.Brightness = Convert.ToInt32(UserProtocols[1]);
            this.Contrast = Convert.ToInt32(UserProtocols[2]);
            this.Volume = Convert.ToInt32(UserProtocols[4]);
            this.Led = Convert.ToInt32(UserProtocols[5]);
            this.Microphone = Convert.ToInt32(UserProtocols[6]);
            this.Proximity = Convert.ToInt32(UserProtocols[7]);
            this.Audio = Convert.ToInt32(UserProtocols[8]);
            
        }
        // for saving the  is options to the user //
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
        // ANDREWS CAMERA SCHEDULE QUERY THING //
    }
}
