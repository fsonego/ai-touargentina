using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aibot_form.Models;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace aibot_form.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(String message)
        {
            var luisResult = GetPrediction(message).Result;
            return RedirectToAction("Thanks");
        }

        public IActionResult Thanks() {


            return View();
        }

        private async Task<LuisResult> GetPrediction(String message)
        {
            var endpointPredictionkey = _config.GetSection("LuisConfig:endpointPredictionkey").Value;
            var credentials = new ApiKeyServiceClientCredentials(endpointPredictionkey);

            var luisClient = new LUISRuntimeClient(credentials, new System.Net.Http.DelegatingHandler[] { });
            luisClient.Endpoint = _config.GetSection("LuisConfig:Endpoint").Value;

            // public Language Understanding Home Automation app
            var appId = _config.GetSection("LuisConfig:appId").Value;

            // query specific to home automation app
            var query = message;

            // common settings for remaining parameters
            Double? timezoneOffset = _config.GetSection("LuisConfig").GetValue<int>("timezoneOffset");
            var verbose = _config.GetSection("LuisConfig").GetValue<bool>("verbose");
            var staging = _config.GetSection("LuisConfig").GetValue<bool>("staging");
            var spellCheck = _config.GetSection("LuisConfig").GetValue<bool>("spellCheck");
            var bingSpellCheckKey = _config.GetSection("LuisConfig:bingSpellCheckKey").Value;
            var log = _config.GetSection("LuisConfig").GetValue<bool>("log");

            // Create prediction client
            var prediction = new Prediction(luisClient);

            // get prediction
            return await prediction.ResolveAsync(appId, query, timezoneOffset, verbose, staging, spellCheck, bingSpellCheckKey, log, CancellationToken.None);
        }

    }    
}
