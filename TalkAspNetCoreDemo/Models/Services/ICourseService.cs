using System.Collections.Generic;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.Models.Services
{
    public interface ICourseService
    {
        Task<IList<CourseViewModel>> SearchAsync(string searchValue);

        Task<IList<CourseViewModel>> GetListAsync();

        Task<DetailCourseViewModel> GetAsync(int id);

        Task<string> Update(int id, string title, string description);

        Task<string> DeleteAsync(int id);

        Task<string> AddLessonAsync(int id);

        Task<string> RemoveLessonAsync(int id);
    }
}