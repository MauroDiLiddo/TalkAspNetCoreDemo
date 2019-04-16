using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalkAspNetCoreDemo.Models.Services;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.Pages.Courses
{
    public class DetailModel : PageModel
    {
        private readonly ICourseService courseService;

        [ViewData]
        public ResultMessage StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public DetailCourseViewModel DetailCourse { get; set; }

        public DetailModel(ICourseService courseService)
        {
            this.courseService = courseService;
            if (StatusMessage == null) StatusMessage = new ResultMessage();
        }

        public async Task OnGetAsync()
        {
            try
            {
                DetailCourse = await courseService.GetAsync(Id);
            }
            catch (Exception ex)
            {
                DetailCourse = new DetailCourseViewModel();
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Error,
                    Message = ex.Message
                };
            }

            return;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var _returnValue = await courseService.DeleteAsync(Id);
                if (string.IsNullOrEmpty(_returnValue))
                {
                    StatusMessage = new ResultMessage()
                    {
                        Type = ResultType.Warning,
                        Message = string.Format("Il corso con Identificativo {0} non è stato trovato", Id)
                    };
                    return Page();
                }

                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = string.Format("Il corso {0} è stato eliminato come richiesto", _returnValue)
                };

                return RedirectToPage("Index");
            }
            catch { }

            return Page();
        }

    }
}