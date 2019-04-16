using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Titolo")]
        public string Title { get; set; }

        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Valutazione Media")]
        public double Rating { get; set; }

        [Display(Name = "Numero di Lezioni")]
        public int LessonCount { get; set; }

        [Display(Name ="Durata Corso")]
        public TimeSpan CourseDuration { get; set; }

        public Price CoursePrice { get; set; }

        [Display(Name = "Docente")]
        public string Author { get; set; }
    }
}
