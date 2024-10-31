using AplicacionClima.Models;
using AplicacionClima.Services;

namespace AplicacionClima;

public partial class PaginaClima : ContentPage
{
    public List<Models.List> ListaTiempo;
    private double latitude;
    private double longitude;
    public PaginaClima()
    {
        InitializeComponent();
        ListaTiempo = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);



    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude; 
        longitude = location.Longitude;
    }

    private async void Taplocation_Tapped(object sender,EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);

    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ServicioApi.GetWeather(latitude, longitude);
        UpdateUI(result);
       
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var responde = await DisplayPromptAsync(title: "", message: "", placeholder: "Buscar ciudad", accept: "Buscar", cancel: "Cancelar");
        if (responde != null) 
        {
            GetWeatherDataByCity(responde);
        }
    }

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ServicioApi.GetWeatherByCity(city);
        UpdateUI(result);
       
    }

    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            ListaTiempo.Add(item);
        }
        descrip.ItemsSource = ListaTiempo;

        Ciudad.Text = result.city.name;
        descripcion.Text = result.list[0].weather[0].description;
        temperatura.Text = result.list[0].main.temperature + "°C";
        gota.Text = result.list[0].main.humidity + "%";
        viento.Text = result.list[0].wind.speed + "Km/h";
        ImgViento.Source = result.list[0].weather[0].fullIconUrl;
    }
}

