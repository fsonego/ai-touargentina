using System;
using System.Threading;
using System.Threading.Tasks;
using aibot_console.Models;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System.Linq;

namespace aibot_console
{
    class Program
    {
        static void Main(string[] args)
        {

            var _db = new aibotContext();
            var randomizer = new Random();

            OptionCardModel cardResult = (from opt in _db.Options
                                          where opt.Type == "WELCOME"
                                          let rand = randomizer.Next()
                                          orderby rand
                                          select new OptionCardModel() {
                                              Title = opt.Title,
                                              Options = (from opt2 in _db.Options where opt2.ParentOptionId == opt.OptionId select opt2).ToList()
                                          }).Take(1).SingleOrDefault(); ;


            Console.WriteLine(cardResult.Title);

            foreach (Options op in cardResult.Options) {
                Console.WriteLine(op.Title);
            }

            var luisResult = GetPrediction().Result;

            // Display query
            Console.WriteLine("Query:'{0}'", luisResult.Query);

            // Display most common properties of query result
            Console.WriteLine("Top intent is '{0}' with score {1}", luisResult.TopScoringIntent.Intent, luisResult.TopScoringIntent.Score);

            // Display all entities detected in query utterance
            foreach (var entity in luisResult.Entities)
            {
                Console.WriteLine("{0}:'{1}' begins at position {2} and ends at position {3}", entity.Type, entity.Entity, entity.StartIndex, entity.EndIndex);
            }

            Console.Write("done");

        }

        static async Task<LuisResult> GetPrediction()
        {
            var endpointPredictionkey = "5cfeca8bb6734baf81b8c65f71635515";
            var credentials = new ApiKeyServiceClientCredentials(endpointPredictionkey);

            var luisClient = new LUISRuntimeClient(credentials, new System.Net.Http.DelegatingHandler[] { });
            luisClient.Endpoint = "https://westus.api.cognitive.microsoft.com";

            // public Language Understanding Home Automation app
            var appId = "344b9f85-5975-4edc-9e66-56cdc7013b9a";
             
            // query specific to home automation app
            var query = "probando";

            // common settings for remaining parameters
            Double? timezoneOffset = 0;
            var verbose = true;
            var staging = true;
            var spellCheck = false;
            String bingSpellCheckKey = null;
            var log = true;

            // Create prediction client
            var prediction = new Prediction(luisClient);

            // get prediction
            return await prediction.ResolveAsync(appId, query, timezoneOffset, verbose, staging, spellCheck, bingSpellCheckKey, log, CancellationToken.None);
        }
    }
}
