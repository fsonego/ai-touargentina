// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace aitour_luis
{
    public class MainDialog : ComponentDialog
    {
        private readonly ConversationState _conversationState;
        private readonly UserState _userState;

        public MainDialog(UserState userState, ConversationState conversationState)
            : base(nameof(MainDialog))
        {
            _userState = userState;
            _conversationState = conversationState;

            AddDialog(new TopLevelDialog());
            AddDialog(new OtherLevelDialog(conversationState));

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                InitialStepAsync,
                //FinalStepAsync,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var conversationStateAccessors = _conversationState.CreateProperty<ConversationData>(nameof(ConversationData));
            var conversationData = await conversationStateAccessors.GetAsync(stepContext.Context, () => new ConversationData());

            if (conversationData.OnDialog == "OtherDialog")
            {
                return await stepContext.BeginDialogAsync(nameof(OtherLevelDialog), null, cancellationToken);
            }
            else {
                return await stepContext.BeginDialogAsync(nameof(TopLevelDialog), null, cancellationToken);
            }
            
        }

    
    }
}
