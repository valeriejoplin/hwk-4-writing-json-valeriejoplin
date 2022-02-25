using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Problem_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnplay.IsEnabled = false;
            btnstop.IsEnabled = false;
            MED.LoadedBehavior = MediaState.Manual;
        }

      

       

        

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {
            RandomVideoAPI video;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://pcbstuou.w27.wh-2.com/webservices/3033/api/random/video").Result;
                if (response.IsSuccessStatusCode)
                {
                    video = JsonConvert.DeserializeObject<RandomVideoAPI>(response.Content.ReadAsStringAsync().Result);
                    MED.Source = new Uri(video.url);
                    btnplay.Content = "Play";
                    btnplay.IsEnabled = true;
                    btnstop.IsEnabled = true;

                }
            }
        }

        private void btnplay_Click(object sender, RoutedEventArgs e)
        {
            string status = btnplay.Content.ToString().ToLower();
            switch (status)
            {
                case "play":
                    MED.Play();
                    btnplay.Content = "Pause";
                    break;
                case "pause":
                    MED.Pause();
                    btnplay.Content = "Play";
                    break;
                default:
                    break;
            }
        }

        private void btnstop_Click(object sender, RoutedEventArgs e)
        {
            MED.Stop();
        }
    }
}
