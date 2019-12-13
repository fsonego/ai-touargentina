// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace aitour_luis
{
    using System.Collections.Generic;

    /// <summary>Contains information about a user.</summary>
    public class UserProfile
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // The list of companies the user wants to review.
        public List<string> CompaniesToReview { get; set; } = new List<string>();

        public bool RequestPaymentFromWorkflow{ get; set; }
    }
}
