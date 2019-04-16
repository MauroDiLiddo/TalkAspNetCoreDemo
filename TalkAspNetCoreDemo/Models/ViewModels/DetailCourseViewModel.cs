using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class DetailCourseViewModel:CourseViewModel
    {
        public IList<LessonViewModel> Lessons { get; set; }
    }
}
