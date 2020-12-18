using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;



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
        private static byte[] array;

        private async void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            if(productNameBox!=null&&categoryBox!=null&&priceBox!=null&&available!=null&&array!=null)
            {
                if (request.CheckIfExist(productNameBox.Text) == false)
                {
                    int pAvailable = 0;
                    if (available.SelectedIndex == 0)
                    {
                        pAvailable = 1;
                    }
                    else
                    {
                        pAvailable = 0;
                    }
                    request.AddItem(productNameBox.Text, categoryBox.SelectedItem.ToString(), double.Parse(priceBox.Text), pAvailable, array);
                }
                else
                {
                    var messageDialog = new MessageDialog("Product already Exists");
                    await messageDialog.ShowAsync();
                }

            }
            else
            {
                var messageDialog = new MessageDialog("Fill in all the details!");
                await messageDialog.ShowAsync();
            }


        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void ImagePicker_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            
            if(file != null)
            {
                ImagePicker.Content = file.DisplayName;
                IBuffer buffer = await FileIO.ReadBufferAsync(file);
                array = buffer.ToArray();
                
            }

        }
    }
}
