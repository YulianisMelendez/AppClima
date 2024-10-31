using AplicacionClima.Models;
using Newtonsoft.Json;

namespace AplicacionClima.Services
{
    public static class ServicioApi
    {
        public static async Task<Root> GetWeather(double Latitude, double Longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=5531cf04e824fcc9a672aed81b5f5d8e", Latitude, Longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root> GetWeatherByCity(String city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=5531cf04e824fcc9a672aed81b5f5d8e", city));
            return JsonConvert.DeserializeObject<Root>(response);
        }

    }






}
