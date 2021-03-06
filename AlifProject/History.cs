
namespace AlifProject
{
    using System;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;

    public static class History
    {
        private static int ID { get; set; }
        private static int Amount { get; set; }
        private static int Delays { get; set; }
        private static string State { get; set; }


        public static void AddApplicationToHistory(int ApplicationID, double CreditAmount, int ClientID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            string commandText = $"Insert into History([ID], [State], [Amount], [ClientID]) Values({ApplicationID}, 'Open', {CreditAmount}, {ClientID})";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.ExecuteNonQuery();
        }
        public static int checkForClientHistory(int ClientID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            int sumApplications = 0;

            string commandText = $"select ID from History where ClientID = {ClientID} and State = 'Closed'";
            SqlCommand command = new SqlCommand(commandText, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                sumApplications++;
            }
            connection.Close();
            reader.Close();
            return sumApplications;
        }
        public static void showApplicationsByClientID(int ClientID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            string commandText = $"select ID, Amount, State, Delays from History";
            SqlCommand command = new SqlCommand(commandText, connection);

            SqlDataReader reader = command.ExecuteReader();

            int amount = reader.FieldCount;
            System.Console.WriteLine($"You have {amount} of Payments:");

            while (reader.Read())
            {
                string historyID = reader.GetValue(0).ToString();
                string historyAmount = reader.GetValue(0).ToString();
                string historyState = reader.GetValue(0).ToString();
                string historyDelays = reader.GetValue(0).ToString();
                System.Console.WriteLine($"Payment with ID = {historyID}. You have to pay {historyAmount} and it is {historyState}");
            }
            connection.Close();
            reader.Close();
        }
    }
}
