using System;
using System.Collections.Generic;
using System.Text;

namespace aibot_console.Models
{
    public class OptionCardModel
    {
        public string Title { get; set; }

        public IList<Options> Options { get; set; }
    }
}
