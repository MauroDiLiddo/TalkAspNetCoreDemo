using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Data.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description  { get; set; }

        public TimeSpan Duration { get; set; }

        public Course Course { get; set; }
    }
}
