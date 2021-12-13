using Azure.Core;
using Azure.Identity;

using Demo.WebUI.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(IHttpClientFactory clientFactory, ILogger<HomeController> logger)
        {
            _logger = logger;
            _clientFactory = clientFactory;

            //HttpClient.BaseAddress = new Uri("https://rutzscodev-demo-webapp-api-auth-api-ci.azurewebsites.net/");
            //HttpClient.BaseAddress = new Uri("https://rutzsco-demo-webapp-api-auth-api-ci2.azure-api.net/");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Privacy()
        {
            var tokenCredential = new DefaultAzureCredential();
            var accessToken = tokenCredential.GetToken(new TokenRequestContext(scopes: new string[] { "api://f85c6bd9-11e3-45b2-8e49-f719766bb99e" + "/.default" }) { });
            List<WeatherForecast> weatherForecast = null;

            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            HttpResponseMessage response = await client.GetAsync("https://rutzsco-demo-webapp-api-auth-api-ci2.azure-api.net/WeatherForecast");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherForecast = JsonSerializer.Deserialize<List<WeatherForecast>>(content);
            }

            return View(new PrivacyViewModel() { AccessToken = accessToken.Token, WeatherForecast = weatherForecast });
        }

        //[AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
