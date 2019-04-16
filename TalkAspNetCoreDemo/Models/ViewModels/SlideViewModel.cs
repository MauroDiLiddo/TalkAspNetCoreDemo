using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class SlideViewModel
    {
        public string UrlImage { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsFromDbContext { get; set; }
    }
}
