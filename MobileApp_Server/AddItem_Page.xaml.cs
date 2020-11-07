using Microsoft.Data.Sqlite;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileApp_Server
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddItem_Page : Page
    {
        private DatabaseRequest request = new DatabaseRequest();
        public AddItem_Page()
        {
            this.InitializeComponent();
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            int pAvailable = 0;
            if(available.SelectedIndex== 0)
            {
                pAvailable = 1;
            }
            else
            {
                pAvailable = 0;
            }
            request.AddItem(productNameBox.Text,categoryBox.SelectedItem.ToString(), double.Parse(priceBox.Text),pAvailable);
       
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    
    }
}
