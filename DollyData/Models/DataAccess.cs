using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Windows.Storage;

namespace DollyData.Models
{
    public static class DataAccess
    {

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("DollyData.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String dollTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS doll (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "name TEXT, description TEXT, lineName TEXT, " +
                    "amount INTEGER, imagePath TEXT, isFavorite INTEGER, " +
                    "companyId INTEGER, FOREIGN KEY (companyId) REFERENCES company(id));";

                String companyTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS company (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "name TEXT)";


                SqliteCommand createDollTable = new SqliteCommand(dollTableCommand, db);
                SqliteCommand createCompanyTable = new SqliteCommand(companyTableCommand, db);

                createCompanyTable.ExecuteNonQuery();
                createDollTable.ExecuteNonQuery();
            }
        }

        public static void AddDollData(string name, string description, string lineName, int amount, string imagePath, bool isFavorite, int companyId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO doll (name, description, lineName, amount, imagePath, isFavorite, companyId) VALUES (@name, @description, @lineName, @amount, @imagePath, @isFavorite, @companyId);";
                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@description", description);
                insertCommand.Parameters.AddWithValue("@lineName", lineName);
                insertCommand.Parameters.AddWithValue("@amount", amount);
                insertCommand.Parameters.AddWithValue("@imagePath", imagePath);
                insertCommand.Parameters.AddWithValue("@isFavorite", isFavorite);
                insertCommand.Parameters.AddWithValue("@companyId", companyId);

                insertCommand.ExecuteNonQuery();
            }

        }

        public static List<Doll> GetAllDollData()
        {
            List<Doll> entries = new List<Doll>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT * from doll", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    string name = query.GetString(0);
                    string description = query.GetString(1);
                    string lineName = query.GetString(2);
                    int amount = query.GetInt32(3);
                    string imagePath = query.GetString(4);
                    bool isFavorite = bool.Parse(query.GetInt32(5).ToString());
                    int companyId = query.GetInt32(6);

                    Doll newDoll = new Doll(name, description, lineName, amount, imagePath, isFavorite, companyId);

                    entries.Add(newDoll);
                }
            }

            return entries;
        }

    }
}
