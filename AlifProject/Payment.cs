
namespace AlifProject
{
    using System;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;
    public class Payment
    {
        private int ID { get; set; }
        private int ClientID { get; set; }
        private string DatePayment { get; set; }
        private double AmountShouldPay { get; set; }
        private int Delays { get; set; }
        private double AmountPayed { get; set; }
        private string LastPayment { get; set; }

        public Payment(int ClientID)
        {
            this.ClientID = ClientID;
        }
        public Payment(int ClientID, int ID)
        {
            this.ClientID = ClientID;
            this.ID = ID;
        }
        public Payment(int ClientID, double AmountPayed, string LastPayment)
        {
            this.AmountPayed = AmountPayed;
            this.ClientID = ClientID;
            this.LastPayment = LastPayment;
        }

        public void showPaymentGraphByClientID()
        {

            int clientID = this.ClientID;
            int appPaymentID = this.ID;

            if (IsPaymentIDExist() == true)
            {

                SqlConnection connection = new SqlConnection(Connection.connectionString);

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();

                System.Console.WriteLine("ApplicationID\t\tPaymentDate\t\tAmount\t\tDelays");

                string commandText1 = $"Select PercentCredit, Goal from Applications where ID = {appPaymentID} and ClientID = {clientID}";

                SqlCommand command1 = new SqlCommand(commandText1, connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                string percent = "";
                string goal = "";
                while (reader1.Read())
                {
                    percent = reader1.GetValue(0).ToString();
                    goal = reader1.GetValue(1).ToString();
                }
                reader1.Close();

                if (percent != "")
                {
                    percent = " With " + percent + "% per term";
                }
                if (goal != "")
                {
                    goal = " for " + goal;
                }
                string commandText = $"Select ID, DatePayment, AmountShouldPay, Delays from Payment where ID = {appPaymentID}";
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlDataReader reader = command.ExecuteReader();

                double sumAmount = 0;

                while (reader.Read())
                {
                    string applicationID = reader.GetValue(0).ToString();
                    string datePayment = reader.GetValue(1).ToString().Substring(0, 10);
                    string amountShouldPay = reader.GetValue(2).ToString().Substring(0, 5);
                    string delays = reader.GetValue(3).ToString();
                    sumAmount += double.Parse(amountShouldPay);
                    System.Console.Write(appPaymentID);

                    for (int i = 0; i < 13 - applicationID.Length; i++)
                    {
                        System.Console.Write(" ");
                    }
                    Console.Write("\t\t" + datePayment);
                    for (int i = 0; i < 3; i++)
                    {
                        System.Console.Write(" ");
                    }
                    Console.Write("\t\t" + amountShouldPay);
                    Console.Write("\t\t" + delays + "\n");
                }
                Console.WriteLine("\t\t\t\t\t\t" + sumAmount.ToString() + percent + goal);
                connection.Close();
                reader.Close();
            }
            else
            {
                ConsoleShow.Red("No such application found\n");
            }
        }

        public bool IsPaymentIDExist()
        {

            int id = this.ClientID;
            id++;
            bool isExist = false;

            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string commandText = $"Select * from Payment where ClientID = {id}";

            SqlCommand command = new SqlCommand(commandText, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.FieldCount > 1)
            {
                isExist = true;
            }
            reader.Close();
            connection.Close();
            return isExist;
        }
        public void Pay(double amount)
        {

            if (IsPaymentIDExist() == true)
            {
                //! Payment
                SqlConnection connection = new SqlConnection(Connection.connectionString);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();

                string commandText = $"select * from Payment where ClienID = {ClientID}";

                SqlCommand command = new SqlCommand(commandText, connection);

                SqlDataReader reader = command.ExecuteReader();

                string datePayment = "", lastPaymet = "";
                double amountShouldPay;

                while (reader.Read() && amount > 0)
                {
                    int id = int.Parse(reader.GetValue(0).ToString().Trim());
                    datePayment = reader.GetValue(2).ToString().Trim();
                    lastPaymet = reader.GetValue(5).ToString().Trim();
                    amountShouldPay = double.Parse(reader.GetValue(3).ToString().Trim());
                    if (amountShouldPay == 0)
                    {
                        continue;
                    }
                    if (amountShouldPay > 0)
                    {
                        if (amountShouldPay < amount)
                        {
                            amountShouldPay = 0;
                            string commandText1 = $"update Payment set AmountShoulPay = {amountShouldPay} where ID = {id}";
                            SqlCommand command1 = new SqlCommand(commandText1, connection);
                            command1.ExecuteNonQuery();
                            amount -= amountShouldPay;
                        }
                        else
                        {
                            string commandText1 = $"update Payment set AmountShoulPay = {amountShouldPay} where ID = {id}";
                            SqlCommand command1 = new SqlCommand(commandText1, connection);
                            command1.ExecuteNonQuery();
                            amountShouldPay -= amount;
                            amount = 0;
                        }
                    }
                }
                reader.Close();
            }


        }

    }
}
