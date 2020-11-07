using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MobileApp_Server
{
    public class Products_Class
    {
       
        public string product_Name { get; set; }
        public string category { get; set; }

        public double price { get; set; }
        public ToggleSwitch toggle { get; set; }

    }
}
