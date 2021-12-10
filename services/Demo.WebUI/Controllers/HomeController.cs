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
using System.Threading.Tasks;

namespace Demo.WebUI.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var tokenCredential = new DefaultAzureCredential(includeInteractiveCredentials: true);
            var accessToken = tokenCredential.GetToken(new TokenRequestContext(scopes: new string[] { "api://f85c6bd9-11e3-45b2-8e49-f719766bb99e" + "/.default" }) { });
            
            return View(new PrivacyViewModel() { AccessToken = accessToken.Token});
        }

        //[AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
