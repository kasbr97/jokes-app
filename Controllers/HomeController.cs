using JokesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace JokesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public ConsumeJokeAPI? consumeJoke { get; set; }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Accessed Home Controller Index");
            ConsumeJokeAPI jokeInfo = new ConsumeJokeAPI();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://v2.jokeapi.dev/joke/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("Christmas?blacklistFlags=nsfw,religious,political,racist,sexist,explicit");
                //_logger.LogInformation("inside using statement RES: " + Res);
                if (Res.IsSuccessStatusCode) 
                {   
                    var responseJoke = Res.Content.ReadAsStringAsync().Result;
                    jokeInfo = JsonConvert.DeserializeObject<ConsumeJokeAPI>(responseJoke);
                }
                return View(jokeInfo);
            }
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
