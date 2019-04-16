using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Fullname { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
