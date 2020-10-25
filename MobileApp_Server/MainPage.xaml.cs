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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileApp_Server
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Products_Class> products = new List<Products_Class>();
        private static string databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ProductData.db");
        private SqliteConnection sqliteConnection = new SqliteConnection($"Filename={databasePath}");
        public MainPage()
        {
            this.InitializeComponent();
            try
            {

                sqliteConnection.Open();
            }
            catch
            {
                CreateDatabase();
            }
           
        }

        private async void CreateDatabase()
        {
            
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("ProductData.db");
            
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
