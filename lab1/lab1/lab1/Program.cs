using System;
using System.Text.RegularExpressions;
using Npgsql;

namespace lab1
{
    class Program
    {
        static void Main()
        {
            string connectionString = "Host=localhost;Port=5433;Username=postgres;Password=12345678;Database=BankDeposits";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT contributors.first_name, contributors.last_name, contributors.patronymic, COUNT(contributor_id)
                    FROM contributors
                    JOIN bankdeposits
                    ON contributors.id = bankdeposits.contributor_id
                    GROUP BY contributors.first_name, contributors.last_name, contributors.patronymic
                    HAVING COUNT(contributor_id) > 1";


                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstName = reader.GetString(0);
                            string lastName = reader.GetString(1);
                            string patronymic = reader.GetString(2);
                            int count = reader.GetInt32(3);

                            Console.WriteLine($"First Name: {firstName}, Last Name: {lastName}, Patronymic: {patronymic}, Count: {count}");
                        }
                    }
                }
            }
        }
    }
}
