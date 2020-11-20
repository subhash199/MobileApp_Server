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
using System.Collections.ObjectModel;
using System.Threading;
using System.Net.Sockets;
using System.Net;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileApp_Server
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        DatabaseRequest request = new DatabaseRequest();
        ServerClass server = new ServerClass();
        ObservableCollection<Products_Class> product = new ObservableCollection<Products_Class>();

        public  MainPage()
        {

            bool isLoaded = false;

            this.InitializeComponent();
            try
            {

                request.CreateDatabase();
                product = request.ShowProducts();
                InventoryList.ItemsSource = request.ShowProducts();
                isLoaded = true;
                


            }
            catch (Exception e)
            {

            }

        }


        private void ServerButton_Toggled(object sender, RoutedEventArgs e)
        {

            if (ServerButton.IsOn == true)
            {
                // server.RunServer();
                Thread serverThread = new Thread(new ThreadStart(server.RunServer));
                serverThread.Start();
            }
            else
            {
                ServerClass.listener.Stop();
            }
        }

        private void ItemAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItem_Page));
        }



        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (IsLoaded == true)
            {
                ToggleSwitch tSwitch = (sender as ToggleSwitch);

                if (tSwitch.IsOn == true)
                {
                    request.AvailabilityAlter(1, tSwitch.Name);
                }
                else
                {
                    request.AvailabilityAlter(0, tSwitch.Name);
                }

            }

        }

        private void SearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            if (product != null)
            {
                InventoryList.ItemsSource = product.Where(a => a.product_Name.ToUpper().Contains(args.QueryText.ToUpper()));
            }
        }
    }
}
