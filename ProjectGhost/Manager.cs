using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGhost
{
    public class Manager
    {
        public int UserID { get; set; }
        public int GhostTypeID { get; set; }
        public int GhostID { get; set; }
        public bool isAddUser { get; set; }
        public string cs = "Server=(localdb)\\ProjectsV13;database=GhostDB;ConnectRetryCount=0;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public void CheckID(string email, string password)
        {
            UserID = 0;
            using (SqlConnection myConn = new SqlConnection(cs))
            {
                SqlCommand login = new SqlCommand();
                login.Connection = myConn;
                myConn.Open();

                login.Parameters.AddWithValue("@email", email);
                login.Parameters.AddWithValue("@password", password);

                login.CommandText = ("[spLogin]");
                login.CommandType = System.Data.CommandType.StoredProcedure;

                var result = login.ExecuteScalar();

                if (result != null)
                {
                    UserID = Convert.ToInt32(result);
                }

                myConn.Close();
            }
        }


        public void CreateAccount(string email, string password, string serialNum, string ghostName)
        {
            int GhostTypeID = 0;
            int GhostID = 0;
            int UserID = 0;

            using (SqlConnection myConn = new SqlConnection(cs))
            {
                //=================================================== gets the ghost type
                SqlCommand GetGhostTypeID = new SqlCommand();
                GetGhostTypeID.Connection = myConn;
                myConn.Open();

                GetGhostTypeID.Parameters.AddWithValue("@ghostName", ghostName);
                GetGhostTypeID.CommandText = ("[spGetGhostType]");
                GetGhostTypeID.CommandType = System.Data.CommandType.StoredProcedure;
                var ghostType = GetGhostTypeID.ExecuteScalar();

                if (ghostType != null)
                {
                    GhostTypeID = Convert.ToInt32(ghostType);
                }
                myConn.Close();

                //=================================================== adds new ghost to table
                SqlCommand CreateGhost = new SqlCommand();
                CreateGhost.Connection = myConn;
                myConn.Open();

                CreateGhost.Parameters.AddWithValue("@serialNum", serialNum);
                CreateGhost.Parameters.AddWithValue("@ghostTypeID", GhostTypeID);
                CreateGhost.CommandText = ("[spAddNewGhost]");
                CreateGhost.CommandType = System.Data.CommandType.StoredProcedure;
                var newGhost = CreateGhost.ExecuteScalar();

                if (newGhost != null)
                {
                    GhostID = Convert.ToInt32(newGhost);
                }
                myConn.Close();

                //=================================================== adds new user
                SqlCommand CreateAccount = new SqlCommand();
                CreateAccount.Connection = myConn;
                myConn.Open();

                CreateAccount.Parameters.AddWithValue("@email", email);
                CreateAccount.Parameters.AddWithValue("@password", password);
                CreateAccount.Parameters.AddWithValue("@ghostID", GhostID);
                CreateAccount.CommandText = ("[spAddNewUser]");
                CreateAccount.CommandType = System.Data.CommandType.StoredProcedure;
                var newUser = CreateAccount.ExecuteScalar();

                if (newUser != null)
                {
                    UserID = Convert.ToInt32(newUser);
                    isAddUser = true;
                }
                myConn.Close();
            }
        }
    }
}
