using System;
using System.Collections.Generic;

namespace aibot_adaptivecards
{
    public partial class Options
    {
        public Guid OptionId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public string Result { get; set; }
        public Guid? ParentOptionId { get; set; }
    }
}
