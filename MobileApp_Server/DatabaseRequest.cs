using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace MobileApp_Server
{
    public class DatabaseRequest
    {
        private SqliteConnection sqliteConnection;
        private SqliteCommand command = new SqliteCommand();
        private SqliteDataReader reader;
        private string databasePath;
        private string databaseName = "ProductData.db";

        private string value = "";
        public async void CreateDatabase()
        {
            //      var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(databaseName);
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            OpenConnection();
            string tableCreate = "CREATE TABLE IF NOT EXISTS products (ProductName TEXT, Category TEXT, Price DECIMAL, Availability INTEGER);";
            command = new SqliteCommand(tableCreate, sqliteConnection);
            command.ExecuteReader();

            //DropTable();
            tableCreate = "CREATE TABLE IF NOT EXISTS users (Name Text, Password Text, Address1 Text, Address2 Text, Postcode Text, Mobile Integer);";
            command = new SqliteCommand(tableCreate, sqliteConnection);
            command.ExecuteReader();

            sqliteConnection.Close();

        }

        public void DropTable()
        {
            string sqlString = "Drop table users;";
            command = new SqliteCommand(sqlString, sqliteConnection);
            command.ExecuteReader();

        }
        public void AddItem(string productNameBox, string categoryBox, double priceBox, int available)
        {
            OpenConnection();
            string commandString = "INSERT INTO products (ProductName, Category, Price, Availability) VALUES (" + "'" + productNameBox + "','" + categoryBox + "'," + priceBox + "," + available + ");";
            command = new SqliteCommand(commandString, sqliteConnection);
            command.ExecuteReader();
            sqliteConnection.Close();
        }

        public ObservableCollection<Products_Class> ShowProducts()
        {
            OpenConnection();
            command = new SqliteCommand("Select * from products;", sqliteConnection);
            reader = command.ExecuteReader();
            var products = new ObservableCollection<Products_Class>();
            while (reader.Read())
            {
                ToggleSwitch toggleSwitch = new ToggleSwitch();

                bool isItOn = true;

                if (reader.GetInt16(3) == 0)
                {
                    isItOn = false;

                }

                toggleSwitch.Name = reader.GetString(0);
                products.Add(new Products_Class() { product_Name = reader.GetString(0), category = reader.GetString(1), price = reader.GetDouble(2), toggle = toggleSwitch, isOn = isItOn });
            }
            sqliteConnection.Close();
            return products;
        }


        public void AvailabilityAlter(int i, string Pname)
        {
            OpenConnection();

            command = new SqliteCommand("update products set Availability = " + i + " where ProductName = " + "'" + Pname + "'", sqliteConnection);
            reader = command.ExecuteReader();
            sqliteConnection.Close();
        }

        public bool CheckIfExist(string PName)
        {
            bool isExist = false;
            OpenConnection();
            command = new SqliteCommand("select ProductName from products;", sqliteConnection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString(0) == PName)
                {
                    isExist = true;
                    reader.Close();
                    break;
                }
            }
            sqliteConnection.Close();
            return isExist;
        }
        internal string LogInUser(string[] info)
        {

            OpenConnection();
            command = new SqliteCommand("select * from users where Name = " + "'" + info[1] + "';", sqliteConnection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                try
                {
                    while (reader.Read())
                    {
                        value = "ok|" + reader.GetString(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" + reader.GetString(4) + "|" + reader.GetInt64(5);

                    }
                }
                catch (Exception e)
                {
                    string error = e.ToString();
                }
                


            }
            sqliteConnection.Close();
            return value;
        }

        public string RegisterUser(string[] info)
        {

            OpenConnection();
            command = new SqliteCommand("select Name from users where Name = " + "'" + info[1] + "';", sqliteConnection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                value = "exist";
                reader.Close();
            }
            else
            {
                try
                {
                    command = new SqliteCommand("Insert into users (Name, Password, Address1, Address2, Postcode, Mobile) values ( " + "'" + info[1] + "'," + "'" + info[2] + "'," + "'" + info[3] + "'," + "'" + info[4] + "'," + "'" + info[5] + "'," + info[6] + " );", sqliteConnection);
                    command.ExecuteReader();
                    value = "ok";
                }
                catch (Exception e)
                {
                    string sl = e.ToString();
                }

            }
            sqliteConnection.Close();
            return value;
        }
        public void OpenConnection()
        {
            databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName);
            sqliteConnection = new SqliteConnection($"Filename={databasePath}");
            sqliteConnection.Open();
        }

    }
}
