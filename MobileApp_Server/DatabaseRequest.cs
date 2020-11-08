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
    
        public async void CreateDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("ProductData.db", CreationCollisionOption.OpenIfExists);
            OpenConnection();
            string tableCreate = "CREATE TABLE IF NOT EXISTS products (ProductName TEXT, Category TEXT, Price DECIMAL, Availability INTEGER)";
            command = new SqliteCommand(tableCreate, sqliteConnection);
            command.ExecuteReader();

            //command = new SqliteCommand("DROP TABLE products", sqliteConnection);
            //command.ExecuteReader();
        }
        public void AddItem(string productNameBox, string categoryBox, double priceBox, int available)
        {
            OpenConnection();
            string commandString = "INSERT INTO products (ProductName, Category, Price, Availability) VALUES ("+"'" + productNameBox + "','" + categoryBox + "'," + priceBox + "," + available + ");";
            command = new SqliteCommand(commandString, sqliteConnection);
            command.ExecuteReader();
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

                if (reader.GetInt16(3) == 1)
                {
                    
                    toggleSwitch.IsOn = true;
                }
                else
                {
                     toggleSwitch = new ToggleSwitch();
                    toggleSwitch.IsOn = false;
                }
                products.Add(new Products_Class() {  product_Name = reader.GetString(0), category = reader.GetString(1), price = reader.GetDouble(2),toggle= toggleSwitch });
            }
            return products;
        }
        public void OpenConnection()
        {
            databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ProductData.db");
            sqliteConnection = new SqliteConnection($"Filename={databasePath}");
            sqliteConnection.Open();
        }

    }
}
