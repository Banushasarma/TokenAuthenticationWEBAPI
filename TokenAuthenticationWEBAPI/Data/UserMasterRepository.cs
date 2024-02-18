using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TokenAuthenticationWEBAPI.Models;


namespace TokenAuthenticationWEBAPI.Data
{
    public class UserMasterRepository
    {
        public UserMaster GetUser(string username, string password)
        {
            try
            {
                UserMaster master = new UserMaster();
                string ConString = "data source=.; database=SECURITY_DB; integrated security=SSPI";
                //string ConString = "Server=UBSHARMA;Database=SECURITY_DB;User Id=sa;password=SQL2022@@@;";
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Creating SqlCommand objcet   
                    SqlCommand cm = new SqlCommand("SELECT * FROM UserMaster", connection);
                    // Opening Connection  
                    connection.Open();
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    while (sdr.Read())
                    {
                        //Console.WriteLine(sdr["UserName"] + ",  " + sdr["UserPassword"] + ",  " + sdr["UserEmailID"]);
                        if (sdr["UserName"].ToString() == username && sdr["UserPassword"].ToString() == password)
                        {
                            master.UserName = username;
                            master.UserPassword = password;
                            master.UserRoles = sdr["UserRoles"].ToString();
                            master.UserID = Convert.ToInt32(sdr["UserID"]);
                            master.UserEmailID = sdr["UserEmailID"].ToString();
                            return master;
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
            return null;
        }

    }
}