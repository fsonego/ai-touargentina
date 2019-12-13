// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace aitour_luis
{
    // Defines a state property used to track conversation data.
    public class ConversationData
    {
        // The time-stamp of the most recent incoming message.
        public string Timestamp { get; set; }

        // The ID of the user's channel.
        public string ChannelId { get; set; }

        // Track whether we have already asked the user's name
        public bool PromptedUserForName { get; set; } = false;
        public string OnDialog { get; set; } = "";

        public string Destination{ get; set; }
        public string Payment { get; set; }

        public string DateFrom { get; set; }
        public string DateUp { get; set; }
        public string TicketType { get; set; }
    }
}
