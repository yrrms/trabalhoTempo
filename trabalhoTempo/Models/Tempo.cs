﻿using SQLite;

namespace trabalhoTempo.Models
{
    public class Tempo
    {
        public string? Title { get; set; }//a interrogação é pra falar que pode ser nulo
        public string? Temperature { get; set; }
        public string? Wind { get; set; }
        public string? Humidity { get; set; }
        public string? Visibility { get; set; }
        public string? Sunrise { get; set; }
        public string? Sunset { get; set; }
        public string? Weather { get; set; }
        public string? WeatherDescription { get; set; }
    }
}
