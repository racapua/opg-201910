using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using opg_201910_interview.Models;
using opg_201910_interview.Services;

namespace opg_201910_interview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
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

        [HttpGet("client/{clientId}")]
        public IActionResult GetClient(string clientId) {
            // Get clients from config 
            var clients = new List<Client>();
            _configuration.GetSection("ClientSettings").Bind(clients);

            // Select client
            var clientQuery = from c in clients where c.ClientId == clientId select c;
            var client = clientQuery.FirstOrDefault();

            if (client != null) {
                var _uploadService = new UploadService();

                var clientFiles = _uploadService.GetClientFiles(client);

                return Ok(clientFiles);
            } else {
                return BadRequest();
            }
        }
    }
}
