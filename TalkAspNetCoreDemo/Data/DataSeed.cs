using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Data.Models;
using TalkAspNetCoreDemo.Models.Configuration;

namespace TalkAspNetCoreDemo.Data
{
    public static class DataSeed
    {
        public static void Seed(IServiceProvider serviceProvider, IHostingEnvironment environment, DataSeedConfiguration dataSeedConfiguration)
        {
            if (!dataSeedConfiguration.RebootDatabase) return;

            var context = serviceProvider.GetRequiredService<TalkDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedMenuItem(context);
            SeedCourseList(context, environment, dataSeedConfiguration);

            context.SaveChanges();
        }

        private static void SeedMenuItem(TalkDbContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM MenuItems");

            var _menuItemList = new List<MenuItem>();
            _menuItemList.Add(new MenuItem()
            {
                Area = string.Empty,
                Page = "/Index",
                Text = " Home",
                ToolTip = "Home",
                IsAsctive = true,
                IconCssClass = IconClass.Home,
            });
            _menuItemList.Add(new MenuItem()
            {
                Area = string.Empty,
                Page = "/Courses/Index",
                Text = " Elenco Corsi",
                ToolTip = "Elenco Corsi",
                IsAsctive = true,
                IconCssClass = IconClass.CourseList,
            });
            _menuItemList.Add(new MenuItem()
            {
                Area = string.Empty,
                Page = "/About",
                Text = " Chi Siamo",
                ToolTip = "ChiSiamo",
                IsAsctive = false,
                IconCssClass = IconClass.AboutUs,
            });
            _menuItemList.Add(new MenuItem()
            {
                Area = string.Empty,
                Page = "/ContactUs",
                Text = " Contattaci",
                ToolTip = "Contattaci",
                IsAsctive = false,
                IconCssClass = IconClass.ContactUs,
            });

            context.MenuItems.AddRange(_menuItemList);
        }

        private static void SeedCourseList(TalkDbContext context, IHostingEnvironment environment, DataSeedConfiguration dataSeedConfiguration)
        {
            #region Delete

            context.Database.ExecuteSqlCommand("DELETE FROM Ratings");
            context.Database.ExecuteSqlCommand("DELETE FROM Lessons");
            context.Database.ExecuteSqlCommand("DELETE FROM Courses");
            context.Database.ExecuteSqlCommand("DELETE FROM Authors");

            #endregion

            #region Setting

            var _fullPreviewPath = environment.WebRootPath + dataSeedConfiguration.ImagePreviewPath;
            var _relativePreviewPath = dataSeedConfiguration.ImagePreviewPath.Replace(@"\", @"/");
            var _filePreviewPath = Directory.GetFiles(_fullPreviewPath);

            var _fullThumbPath = environment.WebRootPath + dataSeedConfiguration.ImageThumbPath;
            var _relativeThumbPath = dataSeedConfiguration.ImageThumbPath.Replace(@"\", @"/");
            var _fileThumbPath = Directory.GetFiles(_fullThumbPath);

            var min = (_filePreviewPath.Length < _fileThumbPath.Length) ? _filePreviewPath.Length : _fileThumbPath.Length;

            var rand = new Random();

            #endregion

            #region Author

            var _author = new Author() { Code = "001", Fullname = "Teacher Test" };
            context.Authors.Add(_author);

            #endregion


            for (int i = 0; i < min; i++)
            {
                #region Course

                var _indexString = (i < 9) ? "0" : string.Empty;
                _indexString += (i + 1).ToString();

                var _price = Convert.ToDecimal(rand.NextDouble() * 10 + 10.55);
                var _discountPrice = _price;
                _discountPrice = (i % 6 != 0) ? _price * 0.75m : _price;

                var _course = new Course()
                {
                    Author = _author,
                    Title = "Test Course - " + _indexString,
                    Description = "Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, Test Course Description, ",
                    ImagePreviewPath = _relativePreviewPath + Path.GetFileName(_filePreviewPath[i]),
                    ImageThumbPath = _relativeThumbPath + Path.GetFileName(_fileThumbPath[i]),
                    FullPrice = _price,
                    DiscountPrice = _discountPrice,
                };

                #endregion

                #region Lesson

                var _lessonList = new List<Lesson>();
                var _lessonCount = rand.Next(5, 20);
                for (int j = 0; j < _lessonCount; j++)
                {
                    var _lesson = new Lesson()
                    {
                        Duration = new TimeSpan(rand.Next(3), rand.Next(60), 0),
                        Title = "Lezione: " + j.ToString(),
                        Description = "Descrizione obbiettivi lezione " + j.ToString(),
                        Course = _course
                    };
                    _lessonList.Add(_lesson);
                }

                #endregion

                #region Rating

                var _ratingList = new List<Rating>();
                var _ratingCount = rand.Next(5, 20);
                var _ratingValutation = 0.5D;
                for (int j = 0; j < _ratingCount; j++)
                {
                    _ratingValutation = Math.Round(rand.NextDouble() * 3, 1);
                    _ratingValutation = (_ratingValutation >= 5) ? 0.5D : _ratingValutation;
                    _ratingValutation = Math.Round(_ratingValutation, 1);
                    var _rating = new Rating()
                    {
                        Valutation = (float)_ratingValutation,
                        Course = _course
                    };
                    _ratingList.Add(_rating);
                }

                #endregion

                _course.Lessons = _lessonList;
                _course.Rating = _ratingList;

                context.Courses.Add(_course);
            }
        }
    }
}
