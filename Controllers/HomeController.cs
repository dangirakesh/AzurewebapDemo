using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AZKeyVault.Models;
using Azure.Identity;
using Azure.Core;
using Azure.Security.KeyVault.Secrets;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace AZKeyVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  IActionResult Index()
        {
           var secretClient = new SecretClient( new Uri(_configuration["AzureKeyVaultUrl"]), new DefaultAzureCredential());
           
            @ViewBag.Keyvalue = secretClient.GetSecret("StorageKey").Value.Name + ": " + secretClient.GetSecret("StorageKey").Value.Value;
            @ViewBag.Keyvalue1 = secretClient.GetSecret("DbconnectionString").Value.Name + ": " + secretClient.GetSecret("DbconnectionString").Value.Value; ;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
