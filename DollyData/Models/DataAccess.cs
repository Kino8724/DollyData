using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Windows.Storage;
using System.Diagnostics;

namespace DollyData.Models
{
    public static class DataAccess
    {

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("DollyData.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                Debug.WriteLine("DB stored here: " + dbpath);
                Debug.WriteLine("Initializing Database");
                db.Open();

                String dollTableCommand = "CREATE TABLE IF NOT EXISTS doll (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, description TEXT, lineName TEXT, amount INTEGER, imagePath TEXT, isFavorite INTEGER, companyId INTEGER, FOREIGN KEY (companyId) REFERENCES company(id));";

                String companyTableCommand = "CREATE TABLE IF NOT EXISTS company (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT)";


                SqliteCommand createDollTable = new SqliteCommand(dollTableCommand, db);
                SqliteCommand createCompanyTable = new SqliteCommand(companyTableCommand, db);

                Debug.WriteLine("Executing queries to create tables");
                createCompanyTable.ExecuteNonQuery();
                createDollTable.ExecuteNonQuery();
            }
            Debug.WriteLine("Initialization complete");
        }

        public static Doll AddDollData(string name, string description, string lineName, int amount, string imagePath, bool isFavorite, int companyId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                int intIsFavorite;

                if(isFavorite == true)
                {
                    intIsFavorite = 1;
                }
                else
                {
                    intIsFavorite = 0;
                }

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = @"INSERT INTO doll (name, description, lineName, amount, imagePath, isFavorite, companyId) VALUES (@name, @description, @lineName, @amount, @imagePath, @isFavorite, @companyId);
                                                SELECT last_insert_rowid();";
                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@description", description);
                insertCommand.Parameters.AddWithValue("@lineName", lineName);
                insertCommand.Parameters.AddWithValue("@amount", amount);
                insertCommand.Parameters.AddWithValue("@imagePath", imagePath);
                insertCommand.Parameters.AddWithValue("@isFavorite", intIsFavorite);
                insertCommand.Parameters.AddWithValue("@companyId", companyId);

                try
                {
                int newDollId = Convert.ToInt32(insertCommand.ExecuteScalar());
                Debug.WriteLine("Doll has been added");
                Doll newDoll = new Doll(newDollId,name,description,lineName,amount,imagePath,isFavorite,companyId);
                return newDoll;
                }catch (Exception ex)
                {
                    Debug.WriteLine("An error has occured : " + ex.ToString());
                    Doll errorDoll = new Doll(0, "Error", "There has been an error: " + ex.TargetSite.ToString(), "...", 0, "Assets\\Square44x44Logo.targetsize-256.png", false);
                    return errorDoll;
                }

            }

        }

        public static Company AddCompanyData(string name)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = @"INSERT INTO company(name) VALUES (@name);
                                                SELECT last_insert_rowid();";
                insertCommand.Parameters.AddWithValue("@name", name);

                Debug.WriteLine("Company has been added");
                int newCompanyId = Convert.ToInt32(insertCommand.ExecuteScalar());

                Company newCompany = new Company(newCompanyId,name);
                Debug.WriteLine("NEW COMPANY ID AFTER INITIALIZING: " + newCompany.Id);
                return newCompany;
            }
        }

        public static List<Company> GetAllCompanyData()
        {
            List<Company> entries = new List<Company>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DollyData.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM company", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                
                while (query.Read())
                {
                    int id = query.GetInt32(0);
                    string name = query.GetString(1);

                    Company newCompany = new Company(id,name);

                    entries.Add(newCompany);
                }
            }
            Debug.WriteLine("Add Companies have been gotten");
            return entries;


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
                    int id = query.GetInt32(0);
                    string name = query.GetString(1);
                    string description = query.GetString(2);
                    string lineName = query.GetString(3);
                    int amount = query.GetInt32(4);
                    string imagePath = query.GetString(5);
                    bool isFavorite = Convert.ToBoolean(query.GetInt32(6));
                    int companyId = query.GetInt32(7);

                    Doll newDoll = new Doll(id, name, description, lineName, amount, imagePath, isFavorite, companyId);

                    entries.Add(newDoll);
                }
            }

            Debug.WriteLine("All doll data has been gotten");
            return entries;
        }

    }
}
