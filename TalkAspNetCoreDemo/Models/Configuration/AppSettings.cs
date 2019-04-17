using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.Configuration
{
    public class AppSettings
    {
        public string ImageCoursePath { get; set; }

        public string ImageAuthorPath { get; set; }

        public bool IsMultiCurrency { get; set; }

        public CurrencySupported CurrencySupported { get; set; }
    }

    public class CurrencySupported
    {
        public string Default { get; set; }

        public IList<string> Others { get; set; }
    }
}
