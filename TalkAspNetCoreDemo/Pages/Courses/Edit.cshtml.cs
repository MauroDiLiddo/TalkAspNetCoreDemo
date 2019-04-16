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
    public class EditModel : PageModel
    {
        private readonly ICourseService courseService;

        [ViewData]
        public ResultMessage StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public DetailCourseViewModel DetailCourse { get; set; }

        public EditModel(ICourseService courseService)
        {
            this.courseService = courseService;
            if (StatusMessage == null) StatusMessage = new ResultMessage();
        }

        public async Task<IActionResult> OnGetAsync()
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

            return Page();
        }





        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var _id = int.Parse(Request.Form["Id"]);
                var _title = Request.Form["DetailCourse.Title"];
                var _description = Request.Form["DetailCourse.Description"];

                var _resultValue = await courseService.Update(_id, _title, _description);
                DetailCourse = await courseService.GetAsync(_id);

                if (string.IsNullOrEmpty(_resultValue))
                {
                    StatusMessage = new ResultMessage()
                    {
                        Type = ResultType.Warning,
                        Message = "Corso non aggiornato"
                    };
                }
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = "Corso aggiornato come richiesto"
                };
            }
            catch (Exception ex)
            {
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Error,
                    Message = ex.Message
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                var _resultValue = await courseService.DeleteAsync(Id);
                if (string.IsNullOrEmpty(_resultValue))
                {
                    StatusMessage = new ResultMessage()
                    {
                        Type = ResultType.Warning,
                        Message = "Corso richiesto non cancellato"
                    };
                }
                DetailCourse = await courseService.GetAsync(Id);
            }
            catch (Exception ex)
            {
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Error,
                    Message = ex.Message
                };
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAddLesson()
        {
            try
            {
                var _resultValue = await courseService.AddLessonAsync(Id);
                if (string.IsNullOrEmpty(_resultValue))
                {
                    StatusMessage = new ResultMessage()
                    {
                        Type = ResultType.Warning,
                        Message = "Lezione richiesta non aggiunta"
                    };
                }
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = string.Format("Lezione {0} aggiunta", _resultValue)
                };
                DetailCourse = await courseService.GetAsync(Id);
            }
            catch (Exception ex)
            {
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Error,
                    Message = ex.Message
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveLesson()
        {
            try
            {
                var _resultValue = await courseService.RemoveLessonAsync(DetailCourse.Id);
                if (string.IsNullOrEmpty(_resultValue))
                {
                    StatusMessage = new ResultMessage()
                    {
                        Type = ResultType.Warning,
                        Message = "Lezione richiesta non eliminata"
                    };
                    DetailCourse = await courseService.GetAsync(Id);
                    return Page();
                }
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = "Lezione richiesta eliminata"
                };
                DetailCourse = await courseService.GetAsync(Id);
            }
            catch (Exception ex)
            {
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Error,
                    Message = ex.Message
                };
            }

            return Page();
        }





    }
}