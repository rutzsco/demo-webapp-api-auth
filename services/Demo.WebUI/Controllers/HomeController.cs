using Azure.Core;
using Azure.Identity;

using Demo.WebUI.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.WebUI.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration config, ILogger<HomeController> logger)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<ActionResult> Index()
        {
            var webAPIScope = _config["WebAPIScope"];
            var webAPIUrl = _config["WebAPIUrl"];

            var tokenCredential = new DefaultAzureCredential();
            var accessToken = tokenCredential.GetToken(new TokenRequestContext(scopes: new string[] { webAPIScope }) { });
            List<WeatherForecast> weatherForecast = null;

            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            HttpResponseMessage response = await client.GetAsync(webAPIUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherForecast = JsonSerializer.Deserialize<List<WeatherForecast>>(content);
            }

            return View(new HomeViewModel(this.HttpContext.User.Claims, accessToken.Token, weatherForecast));
        }

        //[AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
