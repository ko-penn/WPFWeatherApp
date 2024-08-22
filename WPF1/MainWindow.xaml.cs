using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF1.View.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Device.Location;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string request = "https://geocoding.geo.census.gov/geocoder/locations/address?street="+(streetText.Text).Replace(" ","+")+"&zip="+(zipText.Text).Replace(" ", "+")+"&benchmark=Public_AR_Current&format=json";
            using var client = new HttpClient();
            var response = await client.GetAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            var coords = json.SelectToken("result.addressMatches[0].coordinates");
            string longitude = coords.Value<string>("x");
            string latitude = coords.Value<string>("y");

            DateTime endDate = DateTime.Now;
            endDate = endDate.AddDays(3);
            string endDatestr = endDate.ToString("yyyy-MM-dd");
            DateTime endHour = DateTime.Now;
            endHour = endHour.AddHours(4);
            string endHourstr = endHour.ToString("yyyy-MM-ddTH:00");
            request = "https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longitude + "&start_date=" + DateTime.Now.ToString("yyyy-MM-dd") + "&end_date=" + endDatestr + "&start_hour=" + DateTime.Now.ToString("yyyy-MM-ddTH:00") + "&end_hour=" + endHourstr + "&current=apparent_temperature&hourly=apparent_temperature,rain&daily=apparent_temperature_max,apparent_temperature_min,precipitation_probability_max&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FNew_York";
            response = await client.GetAsync(request);
            result = await response.Content.ReadAsStringAsync();
            json = JObject.Parse(result);
            area.Text = longitude+ ", " +latitude;
            currentTemp.Text = (json.SelectToken("current.apparent_temperature") ?? 0).ToString() + "°F";
            time.SetBox1Text((DateTime.Now).ToString("h:00"));
            time.SetBox2Text((DateTime.Now.AddHours(1)).ToString("h:00"));
            time.SetBox3Text((DateTime.Now.AddHours(2)).ToString("h:00"));
            time.SetBox4Text((DateTime.Now.AddHours(3)).ToString("h:00"));
            time.SetBox5Text((DateTime.Now.AddHours(4)).ToString("h:00"));
            temp.SetBox1Text((json.SelectToken("hourly.apparent_temperature[0]") ?? 0).ToString()+ "°F");
            temp.SetBox2Text((json.SelectToken("hourly.apparent_temperature[1]") ?? 0).ToString()+ "°F");
            temp.SetBox3Text((json.SelectToken("hourly.apparent_temperature[2]") ?? 0).ToString()+ "°F");
            temp.SetBox4Text((json.SelectToken("hourly.apparent_temperature[3]") ?? 0).ToString()+ "°F");
            temp.SetBox5Text((json.SelectToken("hourly.apparent_temperature[4]") ?? 0).ToString()+ "°F");
            day0.SetBox1Text("Today");
            day1.SetBox1Text((DateTime.Now.AddDays(1)).ToString("dddd"));
            day2.SetBox1Text((DateTime.Now.AddDays(2)).ToString("dddd"));
            day3.SetBox1Text((DateTime.Now.AddDays(3)).ToString("dddd"));
            day0.SetBox2Text((json.SelectToken("daily.apparent_temperature_min[0]") ?? 0).ToString() + "/" + (json.SelectToken("daily.apparent_temperature_max[0]") ?? 0).ToString());
            day1.SetBox2Text((json.SelectToken("daily.apparent_temperature_min[1]") ?? 0).ToString() + "/" + (json.SelectToken("daily.apparent_temperature_max[1]") ?? 0).ToString());
            day2.SetBox2Text((json.SelectToken("daily.apparent_temperature_min[2]") ?? 0).ToString() + "/" + (json.SelectToken("daily.apparent_temperature_max[2]") ?? 0).ToString());
            day3.SetBox2Text((json.SelectToken("daily.apparent_temperature_min[3]") ?? 0).ToString() + "/" + (json.SelectToken("daily.apparent_temperature_max[3]") ?? 0).ToString());
            day0.SetBox3Text((json.SelectToken("daily.precipitation_probability_max[0]") ?? 0).ToString() + "%");
            day1.SetBox3Text((json.SelectToken("daily.precipitation_probability_max[1]") ?? 0).ToString() + "%");
            day2.SetBox3Text((json.SelectToken("daily.precipitation_probability_max[2]") ?? 0).ToString() + "%");
            day3.SetBox3Text((json.SelectToken("daily.precipitation_probability_max[3]") ?? 0).ToString() + "%");
        }

        private void streetText_GotFocus(object sender, RoutedEventArgs e)
        {
            streetText.SelectAll();
            streetText.Foreground = Brushes.Black;
        }

        private void streetText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            streetText.SelectAll();
            streetText.Foreground = Brushes.Black;
        }

        private void zipText_GotFocus(object sender, RoutedEventArgs e)
        {
            zipText.SelectAll();
            zipText.Foreground = Brushes.Black;
        }

        private void zipText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            zipText.SelectAll();
            zipText.Foreground = Brushes.Black;
        }
    }
}