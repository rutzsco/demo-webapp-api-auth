using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Demo.WebUI.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<Claim> claims, string accessToken, List<WeatherForecast> weatherForecast)
        {
            Claims = claims ?? throw new ArgumentNullException(nameof(claims));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            WeatherForecast = weatherForecast ?? throw new ArgumentNullException(nameof(weatherForecast));
        }

        public IEnumerable<Claim> Claims { get; set; }

        public string AccessToken { get; set; }

        public List<WeatherForecast> WeatherForecast { get; set; }
    }
}
