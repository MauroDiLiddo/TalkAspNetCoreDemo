using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Data;
using TalkAspNetCoreDemo.Data.Models;
using TalkAspNetCoreDemo.Models.Enums;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.Models.Services
{
    public class CourseService : ICourseService
    {
        private readonly TalkDbContext context;

        public CourseService(TalkDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<CourseViewModel>> SearchAsync(string searchValue)
        {
            return await context.Courses
                                .Select(item => new CourseViewModel()
                                {
                                    Id = item.Id,
                                    Title = item.Title,
                                    Description = item.Description.Substring(0, 90) + "...",
                                    ImagePath = item.ImageThumbPath,
                                    Rating = Math.Round(item.Rating.Average(itemRating => itemRating.Valutation), 1),
                                    LessonCount = item.Lessons.Count(),
                                    CourseDuration = new TimeSpan(item.Lessons.Sum(itemLesson => itemLesson.Duration.Ticks)),
                                    CoursePrice = new Price()
                                    {
                                        FullPrice = new Money(Currency.EUR, item.FullPrice),
                                        DiscountPrice = new Money(Currency.EUR, item.DiscountPrice)
                                    },
                                    Author = item.Author.Fullname
                                })
                                .Where(item => item.Title.Contains(searchValue))
                                .ToListAsync();
        }

        public async Task<IList<CourseViewModel>> GetListAsync()
        {
            return await context.Courses
                        .Select(item => new CourseViewModel()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description.Substring(0, 90) + "...",
                            ImagePath = item.ImageThumbPath,
                            Rating = Math.Round(item.Rating.Average(itemRating => itemRating.Valutation), 1),
                            LessonCount = item.Lessons.Count(),
                            CourseDuration = new TimeSpan(item.Lessons.Sum(itemLesson => itemLesson.Duration.Ticks)),
                            CoursePrice = new Price()
                            {
                                FullPrice = new Money(Currency.EUR, item.FullPrice),
                                DiscountPrice = new Money(Currency.EUR, item.DiscountPrice)
                            },
                            Author = item.Author.Fullname
                        })
                        .ToListAsync();
        }

        public async Task<DetailCourseViewModel> GetAsync(int id)
        {
            return await context.Courses
                        .Select(item => new DetailCourseViewModel()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description,
                            ImagePath = item.ImagePreviewPath,
                            Rating = Math.Round(item.Rating.Average(itemRating => itemRating.Valutation), 1),
                            LessonCount = item.Lessons.Count(),
                            CourseDuration = new TimeSpan(item.Lessons.Sum(itemLesson => itemLesson.Duration.Ticks)),
                            CoursePrice = new Price()
                            {
                                FullPrice = new Money(Currency.EUR, item.FullPrice),
                                DiscountPrice = new Money(Currency.EUR, item.DiscountPrice)
                            },
                            Author = item.Author.Fullname,
                            Lessons = item.Lessons.Select(itemLesson => new LessonViewModel()
                            {
                                Id = itemLesson.Id,
                                Title = itemLesson.Title,
                                Description = itemLesson.Description,
                                Duration = itemLesson.Duration
                            }).
                            OrderBy(itemLesson => itemLesson.Id)
                            .ToList()
                        })
                        .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<string> Update(int id, string title, string description)
        {
            var _course = await context.Courses.FirstOrDefaultAsync(item => item.Id == id);
            _course.Title = title;
            _course.Description = description;

            context.Courses.Update(_course);
            await context.SaveChangesAsync();

            return _course.Id.ToString();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var _course = await context.Courses
                                .Include(item => item.Lessons)
                                .Include(item => item.Rating)
                                .FirstOrDefaultAsync(item => item.Id == id);

            context.Courses.Remove(_course);
            await context.SaveChangesAsync();

            return _course.Id.ToString() + " " + _course.Title;
        }

        public async Task<string> AddLessonAsync(int id)
        {
            var _course = await context.Courses.FirstOrDefaultAsync(item => item.Id == id);
            var _newLesson = new Lesson()
            {
                Duration = new TimeSpan(0, 30, 0),
                Title = "Nuova Lezione per il corso con Id: " + id.ToString(),
                Description = "Descrizione obbiettivi nuova lezione per il corso con Id: " + id.ToString(),
                Course = _course
            };
            await context.Lessons.AddAsync(_newLesson);
            await context.SaveChangesAsync();

            return _newLesson.Id.ToString();
        }

        public async Task<string> RemoveLessonAsync(int id)
        {
            if ((await context.Lessons.Where(item => item.Course.Id == id).ToListAsync()).Count == 1)
                return string.Empty;

            var _lastLesson = await context.Lessons.LastOrDefaultAsync(item => item.Course.Id == id);
            context.Lessons.Remove(_lastLesson);
            await context.SaveChangesAsync();

            return _lastLesson.Id.ToString();
        }
    }
}
