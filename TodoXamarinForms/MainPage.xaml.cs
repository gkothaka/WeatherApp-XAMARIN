using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoXamarinForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        RestService _restService;
      //  Button button = new Button { };

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
            
        }

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text.Trim()}" ;
            // requestUri += "&units=metric&lang=hi";           
           requestUri += $"&units={_uniEntry.Text.Trim()}";
           requestUri += $"&lang={ _langEntry.Text.Trim()}";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
        protected override void OnAppearing()
        {
            OnGetWeatherButtonClicked(null, null);
        }
    }
}
