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
    [Authorize]
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
            if(this.HttpContext.User != null)
            {
                return View(new HomeViewModel(this.HttpContext.User.Claims));
            }
            return View(new HomeViewModel());
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
