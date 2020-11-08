using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MobileApp_Server
{
    public class Products_Class : INotifyPropertyChanged
    {
       
        public string product_Name { get; set; }
        public string category { get; set; }

        public double price { get; set; }
        public ToggleSwitch toggle { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
