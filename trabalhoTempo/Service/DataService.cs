﻿
using trabalhoTempo.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace trabalhoTempo.Service
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisaoDoTempo(string cidade)
        {
            // https://openweathermap.org/current#current_JSON
            // https://home.openweathermap.org/api_keys
            string appId = "6135072afe7f6cec1537d5cb08a5a1a2";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={appId}";

            Tempo tempo = null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    Debug.WriteLine("------------------------------");
                    Debug.WriteLine(json);
                    Debug.WriteLine("------------------------------");

                    //var rascunho = JsonConvert.DeserializeObject(json);

                    var rascunho = JObject.Parse(json);


                    Debug.WriteLine("------------------------------");
                    Debug.WriteLine(rascunho);
                    Debug.WriteLine("------------------------------");

                    DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();

                    //ajustar de utc para gmt

                    tempo = new()
                    {
                        Humidity = (string)rascunho["main"]["humidity"],
                        Temperature = (string)rascunho["main"]["temp"],
                        Title = (string)rascunho["name"],
                        Visibility = (string)rascunho["visibility"],
                        Wind = (string)rascunho["wind"]["speed"],
                        Sunrise = sunrise.ToString(),
                        Sunset = sunset.ToString(),
                        Weather = (string)rascunho["weather"][0]["main"],
                        WeatherDescription = (string)rascunho["weather"][0]["description"]
                    };

                }//fecha if
            }//fecha laço using
            return tempo;
        }//fecha metodo
    }//fecha classe
}
