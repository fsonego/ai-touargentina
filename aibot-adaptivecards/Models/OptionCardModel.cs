using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aibot_adaptivecards
{
    public class OptionCardModel
    {
        public Guid OptionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }
        public Guid? ParentOptionId {get;set;}
        public string Result { get; set; }
        public IList<Options> Options { get; set; }
    }
}
