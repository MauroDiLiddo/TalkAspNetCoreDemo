using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class LessonViewModel
    {
        [Display(Name = "Codice")]
        public int Id { get; set; }

        [Display(Name = "Titolo")]
        public string Title { get; set; }

        [Display(Name = "Descrizione obbiettivo")]
        public string Description { get; set; }

        [Display(Name = "Durata Lezione")]
        public TimeSpan Duration { get; set; }
    }
}
