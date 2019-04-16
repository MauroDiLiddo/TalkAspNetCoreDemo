using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public float Valutation { get; set; }

        public Course Course { get; set; }
    }
}
