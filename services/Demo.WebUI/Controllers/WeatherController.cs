using Azure.Core;
using Azure.Identity;
using Demo.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.WebUI.Controllers
{

    public class WeatherController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public WeatherController(IHttpClientFactory clientFactory, IConfiguration config, ILogger<HomeController> logger)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _config = config;
        }

        // GET: WeatherController
        public async Task<ActionResult> Index()
        {
            var webAPIScope = _config["WebAPIScope"];
            var webAPIUrl = _config["WebAPIUrl"];

            var tokenCredential = new DefaultAzureCredential();
            var accessToken = tokenCredential.GetToken(new TokenRequestContext(scopes: new string[] { webAPIScope }) { });

            _logger.LogInformation($"AccessToken: {accessToken}");
            List<WeatherForecast> weatherForecast = null;

            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            HttpResponseMessage response = await client.GetAsync(webAPIUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherForecast = JsonSerializer.Deserialize<List<WeatherForecast>>(content);
            }

            return View(new WeatherViewModel(this.HttpContext.User.Claims, accessToken.Token, weatherForecast));
        }

    }
}
