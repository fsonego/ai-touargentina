// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace aitour_luis
{
    public class DialogAndWelcomeBot<T> : DialogBot<T> where T : Dialog
    {
        public DialogAndWelcomeBot(ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger, IBotServices botServices)
            : base(conversationState, userState, dialog, logger, botServices)
        {
        }

        protected override async Task OnMembersAddedAsync(
            IList<ChannelAccount> membersAdded,
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                // Greet anyone that was not the target (recipient) of this message.
                // To learn more about Adaptive Cards, see https://aka.ms/msbot-adaptivecards for more details.
                if (member.Id != turnContext.Activity.Recipient.Id)
                {


                    var welcomeCard = new WelcomeCard();
                    welcomeCard.BotName = "Botty";
                    welcomeCard.Title = "Bievenido al bot de viajes";
                    welcomeCard.UrlImage = "https://aibotstoragearg.blob.core.windows.net/img-aitour/bot-avatar_.png";
                    welcomeCard.Description = "Este bot proporciona una conversación compleja, con diálogos mas fluidos. Escribe algo para comenzar.";

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
    }
}
