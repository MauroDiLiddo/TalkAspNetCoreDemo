using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class MenuItemViewModel
    {
        public virtual string Area { get; set; }

        public virtual string Page { get; set; }

        public virtual string Text { get; set; }

        public string ToolTip { get; set; }

        public string IconCssClass { get; set; }
    }
}
