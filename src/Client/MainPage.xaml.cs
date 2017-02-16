using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:5001/login").Result;
            if (!response.IsSuccessStatusCode)
            {
                textBlock.Text += response.StatusCode + "\r\n";
            }
            else
            {
                tokenTextBlock.Text = response.Content.ReadAsStringAsync().Result;
            }
            return;
        }

        private void callApiButton_Click(object sender, RoutedEventArgs e)
        {
            // call api
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenTextBlock.Text);

            var response = client.GetAsync("http://localhost:5001/values").Result;
            if (!response.IsSuccessStatusCode)
            {
                textBlock.Text += response.StatusCode + "\r\n";
            }
            else
            {
                textBlock.Text += response.Content.ReadAsStringAsync().Result + "\r\n";
            }
        }
    }
}
