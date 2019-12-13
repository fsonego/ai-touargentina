// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace aitour_luis
{
    public class OtherLevelDialog : ComponentDialog
    {
        // Define a "done" response for the company selection prompt.
        private const string DoneOption = "done";

        // Define value names for values tracked inside the dialogs.
        private const string UserInfo = "value-userInfo";
        private readonly ConversationState _conversationState;


        public OtherLevelDialog(ConversationState conversationState)
            : base(nameof(OtherLevelDialog))
        {
            _conversationState = conversationState;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));

            //AddDialog(new ReviewSelectionDialog());

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NameStepAsync,
                PassStepAsync,
                PaymentMethodStepAsync,
                EndStepAsync,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private static async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Create an object in which to collect the user's information within the dialog.
            stepContext.Values[UserInfo] = new UserProfile();

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("Ingresa tu nombre") };

            // Ask the user to enter their name.
            
            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }

        private async Task<DialogTurnResult> PassStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Set the user's name to what they entered in response to the name prompt.
            var userProfile = (UserProfile)stepContext.Values[UserInfo];
            userProfile.Name = (string)stepContext.Result;

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("Ingresa tu número de DNI") };

            // Ask the user to enter their age.
            return await stepContext.PromptAsync(nameof(NumberPrompt<int>), promptOptions, cancellationToken);
        }


        private async Task<DialogTurnResult> PaymentMethodStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var userProfile = (UserProfile)stepContext.Values[UserInfo];


            var conversationStateAccessors = _conversationState.CreateProperty<ConversationData>(nameof(ConversationData));
            var conversationData = await conversationStateAccessors.GetAsync(stepContext.Context, () => new ConversationData());

            if (conversationData.Payment == string.Empty)
            {
                var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("Ingresa la forma de pago") };
                userProfile.RequestPaymentFromWorkflow = true;
                return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
            }
            else {

                return await stepContext.NextAsync(new List<string>(), cancellationToken);
            }
            

        }


        private async Task<DialogTurnResult> EndStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            
            var userProfile = (UserProfile)stepContext.Values[UserInfo];
            var conversationStateAccessors = _conversationState.CreateProperty<ConversationData>(nameof(ConversationData));
            var conversationData = await conversationStateAccessors.GetAsync(stepContext.Context, () => new ConversationData());
            
            if (userProfile.RequestPaymentFromWorkflow) { 
                conversationData.Payment = (string)stepContext.Result;
            }

            // Thank them for participating.
            await stepContext.Context.SendActivityAsync(
                MessageFactory.Text($"Gracias por tú reserva {((UserProfile)stepContext.Values[UserInfo]).Name}, DNI: ." +
                                    $" El viaje fue programado a {conversationData.Destination} con el tipo de ticket {conversationData.TicketType} " +
                                    $"desde la fecha {conversationData.DateFrom} hasta {conversationData.DateUp}. " +
                                    $"Sera abondado con: {conversationData.Payment}.")
                                    , cancellationToken);
            conversationData.OnDialog = "";
            // Exit the dialog, returning the collected user information.
            return await stepContext.EndDialogAsync(stepContext.Values[UserInfo], cancellationToken);
        }
    }
}
