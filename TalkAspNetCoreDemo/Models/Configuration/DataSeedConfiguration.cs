using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.Configuration
{
    public class DataSeedConfiguration
    {
        public bool RebootDatabase { get; set; }

        public string ImagePreviewPath { get; set; }

        public string ImageThumbPath { get; set; }
    }
}
