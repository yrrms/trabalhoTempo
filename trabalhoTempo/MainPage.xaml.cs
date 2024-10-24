using trabalhoTempo.Models;
using trabalhoTempo.Service;
using System.Diagnostics;
using System.Linq.Expressions;

namespace trabalhoTempo
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource _cancelTokenSource;
        bool _isCheckingLocation;

        string? cidade;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                _cancelTokenSource = new CancellationTokenSource();

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromMinutes(10));
                Location? location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    lbl_latitude.Text = location.Latitude.ToString();
                    lbl_longitude.Text = location.Longitude.ToString();

                    Debug.WriteLine("-----------------------------------------");
                    Debug.WriteLine(location);
                    Debug.WriteLine("-----------------------------------------");

                }
            }
            catch (FeatureNotSupportedException fnsEX)
            {
                await DisplayAlert("Erro: Dispositivo não suporta", fnsEX.Message, "OK");
            }
            catch (FeatureNotEnabledException fneEX)
            {
                await DisplayAlert("ERRO: localização desabilitada", fneEX.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro: Permissão", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro: ", ex.Message, "OK");
            }//ultimo catch
        }//fecha button clicked

        private async Task<string> GetGeocodeReverseData(double latitude = 47.673988, double longitude = -122.121513)
        {
            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

            Placemark? placemark = placemarks?.FirstOrDefault();

            Debug.WriteLine("-----------------------------------------");
            Debug.WriteLine(placemark?.Locality);
            Debug.WriteLine("-----------------------------------------");

            if (placemark != null)
            {
                cidade = placemark.Locality;

                return
                    $"AdminArea:    {placemark.AdminArea}\n" +
                    $"CountryCode:  {placemark.CountryName}\n" +
                    $"CountryName:  {placemark.CountryName}\n" +
                    $"FeatureName:  {placemark.FeatureName}\n" +
                    $"Locality:     {placemark.Locality}\n" +
                    $"PostalCode:   {placemark.PostalCode}\n" +
                    $"SubAdminArea: {placemark.SubAdminArea}\n" +
                    $"SubLocality:  {placemark.SubLocality}\n" +
                    $"SubThoroughfare:  {placemark.SubThoroughfare}\n" +
                    $"Thoroughfare:  {placemark.Thoroughfare}\n";

            }
            return "Nada";
        }//fewcha metodo GetGeocodeReverseData
         //
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            double latitude = Convert.ToDouble(lbl_latitude.Text);
            double longitude = Convert.ToDouble(lbl_longitude.Text);

            lbl_reverso.Text = await GetGeocodeReverseData(latitude, longitude);
        }


        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {

                if (!String.IsNullOrEmpty(cidade))
                {
                    Tempo? previsao = await DataService.GetPrevisaoDoTempo(cidade);

                    string dados_previsao = "";

                    if (previsao != null)
                    {
                        dados_previsao = $"Humidade:   {previsao.Humidity}\n" +
                                        $"Nascer do Sol: {previsao.Sunrise}\n" +
                                        $"Pôr do Sol: {previsao.Sunset}\n" +
                                        $"Temperatura: {previsao.Temperature}\n" +
                                        $"Titulo: {previsao.Title}\n" +
                                        $"Visibilidade: {previsao.Visibility}\n" +
                                        $"Vento: {previsao.Wind}\n" +
                                        $"Previsao: {previsao.Weather}\n" +
                                        $"Descricao: {previsao.WeatherDescription}";
                         previsao.Data = newDate;
                        await App.Db.Insert(previsao);//insercao no banco
                    }
                    else
                    {
                        dados_previsao = $"Sem dados, previsão nula";
                    }

                    Debug.WriteLine("-------------------------------------");
                    Debug.WriteLine(dados_previsao);
                    Debug.WriteLine("-------------------------------------");

                    lbl_previsao.Text = dados_previsao;


                }// fecha if
                 //colocar no database




            }//fecha try
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }

        } // fecha metodo

        private void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            //mostrar lista

        }


       
    } //fecha classe
}

