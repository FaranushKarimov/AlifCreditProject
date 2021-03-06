
namespace AlifProject
{
    using System;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;

    public class Client
    {
        private int ID { get; set; }
        private string Firstname { get; set; }
        private string Secondname { get; set; }
        private string Middlename { get; set; }
        private string Gender { get; set; }
        private int Age { get; set; }
        private string Citizenship { get; set; } // Tajikistan, Other
        private string Family { get; set; } // Married...etc 
        private string City { get; set; }
        private string District { get; set; }
        private string Street { get; set; }
        private string House { get; set; }

        public Client(int ID)
        {
            this.ID = ID;
        }

        public Client()
        {

        }
        public Client(string Firstname, string Secondname, string Middlename, string Gender, int Age,
                        string Citizenship, string Family, string City, string District, string Street, string House)
        {
            this.Firstname = Firstname;
            this.Secondname = Secondname;
            this.Middlename = Middlename;
            this.Gender = Gender;
            this.Age = Age;
            this.Citizenship = Citizenship;
            this.Family = Family;
            this.City = City;
            this.District = District;
            this.Street = Street;
            this.House = House;
        }

        public void showAllClients()
        {

            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            string commandText = $"Select ID, Firstname, Secondname, Middlename from Client";
            SqlCommand command = new SqlCommand(commandText, connection);

            SqlDataReader reader = command.ExecuteReader();

            System.Console.WriteLine("ID\t\tFirstname\t\tSecondname\t\tMiddlename");
            while (reader.Read())
            {
                string tempId = reader.GetValue(0).ToString();
                string tempFirstname = reader.GetValue(1).ToString().Trim();
                string tempSecondname = reader.GetValue(2).ToString().Trim();
                string tempMiddlename = reader.GetValue(3).ToString().Trim();
                Console.Write(tempId + "\t\t" + tempFirstname);
                for (int i = 0; i < 9 - tempFirstname.Length; i++)
                {
                    System.Console.Write(" ");
                }
                Console.Write("\t\t" + tempSecondname);
                for (int i = 0; i < 10 - tempSecondname.Length; i++)
                {
                    System.Console.Write(" ");
                }
                Console.WriteLine("\t\t" + tempMiddlename);
            }
            reader.Close();
        }

        public void showByClientId(int id)
        {

            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            string commandText = $"Select Firstname, Secondname from Client where ID = {id}";
            SqlCommand command = new SqlCommand(commandText, connection);

            SqlDataReader reader = command.ExecuteReader();
        adminFunctions:
            System.Console.WriteLine("What you want to see:");
            System.Console.WriteLine("1. Applications");
            System.Console.Write("2. Payment graph of Application By ID\n3. Exit\nPlease type reference number: ");
            int adminChoice = int.Parse(Console.ReadLine());
            switch (adminChoice)
            {
                case 1:
                    {
                        Applications application = new Applications(id);
                        application.creditStateView();
                        goto adminFunctions;
                    }
                    break;
                case 2:
                    {
                    appIDChecking:
                        System.Console.WriteLine("Type Application ID: ");
                        int appID;
                        if (!int.TryParse(Console.ReadLine(), out appID))
                        {
                            System.Console.WriteLine("Error: check input");
                            goto appIDChecking;
                        }
                        Payment payment = new Payment(id, appID);
                        if (payment.IsPaymentIDExist())
                        {
                            payment.showPaymentGraphByClientID();
                        }
                        goto adminFunctions;
                    }
                    break;
                case 3:
                    {

                    }
                    break;
            }


            //! Add reader of Client and Application by CLient ID, where ID is Unique
            reader.Close();
            connection.Close();
        }

        public bool AddClient()
        {
            bool isAdded = false;

            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string commandText = $"Insert into Client( [Firstname],[Secondname],[Middlename],[Gender],[Age],[Citizenship],[Family], [City], [District], [Street], [House]) Values ( '{Firstname}', '{Secondname}', '{Middlename}', '{Gender}', {Age}, '{Citizenship}', '{Family}', '{City}', '{District}', '{Street}', '{House}')";

            //! Calculation


            SqlCommand command = new SqlCommand(commandText, connection);

            var result = command.ExecuteNonQuery();

            if (result == 1)
            {
                isAdded = true;
            }
            System.Console.WriteLine("Client Added");
            return isAdded;
        }


    }
}
