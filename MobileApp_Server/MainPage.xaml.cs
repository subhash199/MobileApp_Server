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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileApp_Server
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<Products_Class> products = new List<Products_Class>();
            ToggleSwitch milk_Toggle = new ToggleSwitch();
            products.Add(new Products_Class() { product_ID = 1, product_Name = "Milk", category = "Grocery", price = 1.00, toggle = milk_Toggle });
            for (int i = 0; i < products.Count; i++)
            {
                ProductsListView.ItemsSource = products;
                
               // ProductsListView.Items.Add(products[i]);
            }

        }

        private void ServerButton_Toggled(object sender, RoutedEventArgs e)
        {

        }
    }
}
