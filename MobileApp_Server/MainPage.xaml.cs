using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Microsoft.Data.Sqlite;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Storage.Pickers;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileApp_Server
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Products_Class> products = new List<Products_Class>();
        private static string databasePath;
        public static SqliteConnection sqliteConnection;
        public static SqliteCommand command = new SqliteCommand();
        public static SqliteDataReader reader;
       
        public MainPage()
        {
            
            this.InitializeComponent();
            try
            {
                CreateDatabase();
                ShowProducts();
              
            }
            catch( Exception e)
            {
                MessageDialog dialog = new MessageDialog(e.ToString());
                dialog.ShowAsync();
                
            }
           
        }

        private void ShowProducts()
        {
            command = new SqliteCommand("Select * from products;", sqliteConnection);
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                
                if (reader.GetInt16(4) ==1)
                {
                    ToggleSwitch toggleSwitch = new ToggleSwitch();
                    toggleSwitch.IsOn = true;
                }
                else
                {
                    ToggleSwitch toggleSwitch = new ToggleSwitch();
                    toggleSwitch.IsOn = false;
                }
                products.Add(new Products_Class() { product_ID = reader.GetInt32(0), product_Name = reader.GetString(1), category = reader.GetString(2), price = reader.GetDouble(3), });
            }
        }

        private async void CreateDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("ProductData.db", CreationCollisionOption.OpenIfExists);
            databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ProductData.db");
            sqliteConnection = new SqliteConnection($"Filename={databasePath}");
            sqliteConnection.Open();
            string tableCreate = "CREATE TABLE IF NOT EXISTS products (ProductID INTEGER PRIMARY KEY, ProductName TEXT, Category TEXT, Price DECIMAL, Availability INTEGER)";
            command = new SqliteCommand(tableCreate, sqliteConnection);
            command.ExecuteReader();
           
        }
     
        private  void ServerButton_Toggled(object sender, RoutedEventArgs e)
        {
            if (ServerButton.IsOn == true)
            {

            }
        }

        private void ItemAdd_Click(object sender, RoutedEventArgs e)
        {            
            Frame.Navigate(typeof(AddItem_Page));
        }
    }
}
