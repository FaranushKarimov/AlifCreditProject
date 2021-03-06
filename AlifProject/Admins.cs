
namespace AlifProject
{
    using System;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;
    public class Admins
    {
        public string login { get; set; }
        public string password { get; set; }

        public Admins(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public bool Register()
        {

            bool isRegistered = false;
            SqlConnection connection = new SqlConnection(Connection.connectionString);

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            if (checkLogin() == false)
            {
                isRegistered = false;
            }

            int t = getAdminID();
            if (t == -1)
            {

                string insertCommand = $"Insert into Admins([Login], [Password]) Values ('{login}', '{password}')";
                // adding new user {login}, {password} and {type} of user

                SqlCommand command = new SqlCommand(insertCommand, connection);

                var result = command.ExecuteNonQuery();
                isRegistered = true;
            }
            else
            {
                System.Console.WriteLine("Sorry, you already have account....");
                isRegistered = false;
            }
            return isRegistered;
        }


        public bool Login()
        {

            bool isLogined = false;
            if (checkLogin() == true)
            {
                if (getAdminID() != -1)
                {
                    isLogined = true;
                }
            }
            else
            {
                System.Console.WriteLine("Error: Incorrect type of Login");
            }
            return isLogined;
        }

        public int getAdminID()
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString);

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string getAdminId = $"select ID from Admins where Login = '{login}' and Password = '{password}'";

            SqlCommand command = new SqlCommand(getAdminId, connection);
            SqlDataReader reader = command.ExecuteReader();
            int res = -1;
            while (reader.Read())
            {
                res = int.Parse(reader.GetValue(0).ToString());
            }
            return res;
        }
        public bool checkLogin()
        {

            foreach (var item in login)
            {
                if (item == '+')
                {
                    continue;
                }
                if (item < '0' || item > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
