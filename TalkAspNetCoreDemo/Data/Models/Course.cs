using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Data.Models
{
    public class Course
    {
        public  int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string ImageThumbPath { get; set; }

        public virtual string ImagePreviewPath { get; set; }

        public virtual decimal FullPrice { get; set; }

        public virtual decimal DiscountPrice { get; set; }

        public IEnumerable<Rating> Rating { get; set; }

        public Author Author { get; set; }

        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
