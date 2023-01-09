using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Diagnostics;
using System.Web;
using MongoDB.Driver;
using System.Net;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/");
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string getIP()
        {
            string ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    
        public IActionResult Index()
        {
            var database = client.GetDatabase("demo");
            var tab = database.GetCollection<Table>("data");
            Table table1 = new Table();
            table1.date = DateTime.Now.ToString();
            table1.ip = getIP();
            tab.InsertOne(table1);

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["IP"] = getIP();
            ViewData["Date"] = DateTime.Now;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}