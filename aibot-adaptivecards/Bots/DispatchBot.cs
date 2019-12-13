// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace aibot_adaptivecards
{
    public class DispatchBot : ActivityHandler 
    {
        
        private IBotServices _botServices;

        public DispatchBot(IBotServices botServices)
        {
           
            _botServices = botServices;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
                if (turnContext.Activity.Type == turnContext.Activity.Value != null || turnContext.Activity.Value != string.Empty)
                {

                    var cardData = JsonConvert.DeserializeObject<JsonDataAux>(turnContext.Activity.Value.ToString());
                    var helper = new HelperCards();

                    var response = ((Activity)turnContext.Activity).CreateReply();
                    AdaptiveCard reply;

                    if (cardData.Action == "GALLERYIMAGES")
                    {
                        var cardResult = helper.GetGalleryCard(Guid.Parse(cardData.Selected));
                        reply = cardResult.GetCard();
                    }else if (cardData.Action == "VIDEOCARD"){
                        var cardResult = helper.GetVideoCard(Guid.Parse(cardData.Selected));
                        reply = cardResult.GetCard();
                    }
                    else if (cardData.Action == "FORM")
                    {
                        var cardResult = helper.GetFormCard(Guid.Parse(cardData.Selected));
                        reply = cardResult.GetCard();
                    }
                    else if (cardData.Action == "MDZFORM")
                    {
                        var _formInfo = JsonConvert.DeserializeObject<FormMDZModel>(turnContext.Activity.Value.ToString());
                        var cardResult = helper.GetStandardCard(Guid.Parse("d92c1ecb-1752-4ae3-97de-7b4aef61ee6b"));
                        reply = cardResult.GetCard();
                    }                
                    else {
                            var cardResult = helper.GetStandardCard(Guid.Parse(cardData.Selected));
                            reply = cardResult.GetCard();
                    }
                    
                    var result = new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = reply
                    };

                    response.Attachments = new List<Attachment>() { result };

                    await turnContext.SendActivityAsync(response, cancellationToken);

                }else { 
                    // First, we use the dispatch model to determine which cognitive service (LUIS or QnA) to use.
                    var recognizerResult = await _botServices.Dispatch.RecognizeAsync(turnContext, cancellationToken);
            
                    // Top intent tell us which cognitive service to use.
                    var topIntent = recognizerResult.GetTopScoringIntent();
            
                    // Next, we call the dispatcher with the top intent.
                    await DispatchToTopIntentAsync(turnContext, topIntent.intent, recognizerResult, cancellationToken);
                }
            //}
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {

                    var helper = new HelperCards();
                    var welcomeCard = helper.GetWelcomeCard();
                 
                    var response = ((Activity)turnContext.Activity).CreateReply();
                    var reply = welcomeCard.GetCard();

                    var result = new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = reply
                    };

                    response.Attachments = new List<Attachment>() { result };
                    
                    await turnContext.SendActivityAsync(response, cancellationToken);
                   
                }
            }
        }

        private async Task DispatchToTopIntentAsync(ITurnContext<IMessageActivity> turnContext, string intent, RecognizerResult recognizerResult, CancellationToken cancellationToken)
        {
            switch (intent)
            {
                case "l_HomeAutomation":
                    await ProcessHomeAutomationAsync(turnContext, recognizerResult.Properties["luisResult"] as LuisResult, cancellationToken);
                    break;
                case "l_Weather":
                    await ProcessWeatherAsync(turnContext, recognizerResult.Properties["luisResult"] as LuisResult, cancellationToken);
                    break;
                default:
                    //_logger.LogInformation($"Dispatch unrecognized intent: {intent}.");
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Dispatch unrecognized intent: {intent}."), cancellationToken);
                    break;
            }
        }

        private async Task ProcessHomeAutomationAsync(ITurnContext<IMessageActivity> turnContext, LuisResult luisResult, CancellationToken cancellationToken)
        {
           
            // Retrieve LUIS result for Process Automation.
            var result = luisResult.ConnectedServiceResult;
            var topIntent = result.TopScoringIntent.Intent; 
            
            await turnContext.SendActivityAsync(MessageFactory.Text($"HomeAutomation top intent {topIntent}."), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text($"HomeAutomation intents detected:\n\n{string.Join("\n\n", result.Intents.Select(i => i.Intent))}"), cancellationToken);
            if (luisResult.Entities.Count > 0)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text($"HomeAutomation entities were found in the message:\n\n{string.Join("\n\n", result.Entities.Select(i => i.Entity))}"), cancellationToken);
            }
        }

        private async Task ProcessWeatherAsync(ITurnContext<IMessageActivity> turnContext, LuisResult luisResult, CancellationToken cancellationToken)
        {
            
            // Retrieve LUIS results for Weather.
            var result = luisResult.ConnectedServiceResult;
            var topIntent = result.TopScoringIntent.Intent;
            await turnContext.SendActivityAsync(MessageFactory.Text($"ProcessWeather top intent {topIntent}."), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text($"ProcessWeather Intents detected::\n\n{string.Join("\n\n", result.Intents.Select(i => i.Intent))}"), cancellationToken);
            if (luisResult.Entities.Count > 0)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text($"ProcessWeather entities were found in the message:\n\n{string.Join("\n\n", result.Entities.Select(i => i.Entity))}"), cancellationToken);
            }
        }

    }
}
