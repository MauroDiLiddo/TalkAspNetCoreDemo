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
    public class IndexModel : PageModel
    {
        private readonly ICourseService courseService;

        public IndexModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [ViewData]
        public ResultMessage StatusMessage { get; set; }

        [BindProperty]
        public IEnumerable<CourseViewModel> CourseViews { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                CourseViews = await courseService.GetListAsync();
            }
            catch (Exception ex)
            {
                CourseViews = new List<CourseViewModel>();
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Error,
                    Message = ex.Message
                };
            }
            return;
        }

        [BindProperty(SupportsGet = true)]
        public string Value { get; set; }

        public async Task<IActionResult> OnGetSearchAsync()
        {
            try
            {
                CourseViews = await courseService.SearchAsync(Value);
                StatusMessage = new ResultMessage()
                {
                    Type = ResultType.Info,
                    Message = String.Format("In base alla tua ricerca. Sono stati trovati {0} corsi", CourseViews.Count())
                };

            }
            catch (Exception ex)
            {
                CourseViews = new List<CourseViewModel>();
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