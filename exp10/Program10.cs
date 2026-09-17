using System;
using System.Data;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=StudentDB.db";

        // Create database and table
        using (SqliteConnection con = new SqliteConnection(connectionString))
        {
            con.Open();

            string createTable = @"
                CREATE TABLE IF NOT EXISTS Students
                (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT,
                    Course TEXT
                )";

            using (SqliteCommand cmd = new SqliteCommand(createTable, con))
            {
                cmd.ExecuteNonQuery();
            }

            // Insert sample data
            string insertData = @"
                INSERT OR IGNORE INTO Students VALUES
                (1, 'Harini', 'B.Tech IT'),
                (2, 'Anu', 'B.E CSE'),
                (3, 'Priya', 'B.E ECE')";

            using (SqliteCommand cmd = new SqliteCommand(insertData, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // Disconnected environment
        DataTable table = new DataTable();

        using (SqliteConnection con = new SqliteConnection(connectionString))
        {
            con.Open();

            string query = "SELECT * FROM Students";

            using (SqliteCommand cmd = new SqliteCommand(query, con))
            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                table.Load(reader);
            }
        }

        // Connection is now closed.
        // Data is available in DataTable.

        Console.WriteLine("Student Details");
        Console.WriteLine("-------------------------");

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine(
                "ID: " + row["Id"] +
                "  Name: " + row["Name"] +
                "  Course: " + row["Course"]);
        }
    }
}
