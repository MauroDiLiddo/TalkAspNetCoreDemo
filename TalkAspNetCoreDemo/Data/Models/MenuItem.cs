using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Data.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        public virtual string Area { get; set; }

        public virtual string Page { get; set; }

        public virtual string Text { get; set; }

        public string ToolTip { get; set; }

        public string IconCssClass { get; set; }

        public bool IsAsctive { get; set; }
    }
}
