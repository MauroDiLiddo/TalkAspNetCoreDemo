using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Data;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.Models.Services
{
    public class HeaderService : IHeaderService
    {
        private readonly TalkDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;

        public HeaderService(TalkDbContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IList<MenuItemViewModel>> GetMenu(bool isShowIcon, MenuItemType menuItemType)
        {
            try
            {
                if (menuItemType == MenuItemType.AllActive)
                {
                    return await context.MenuItems
                              .Where(item => item.IsAsctive == true)
                              .OrderBy(item => item.Id)
                              .Select(item => new MenuItemViewModel()
                              {
                                  Area = item.Area,
                                  Page = item.Page,
                                  Text = item.Text,
                                  ToolTip = item.ToolTip,
                                  IconCssClass = (isShowIcon) ? item.IconCssClass : string.Empty
                              })
                              .ToListAsync();
                }
                return await context.MenuItems
                              .OrderBy(item => item.Id)
                              .Select(item => new MenuItemViewModel()
                              {
                                  Area = item.Area,
                                  Page = item.Page,
                                  Text = item.Text,
                                  ToolTip = item.ToolTip,
                                  IconCssClass = (isShowIcon) ? item.IconCssClass : string.Empty
                              })
                              .ToListAsync();
            }
            catch (Exception ex) { }

            return new List<MenuItemViewModel>();
        }

        public IList<SlideViewModel> GetDemoSlider()
        {
            var _slideList = new List<SlideViewModel>();
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/f44242/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/418cf4/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/3ed846/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/42ebf4/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/f49b41/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/f4f141/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });
            _slideList.Add(new SlideViewModel
            {
                UrlImage = "http://placehold.it/800x600/8e41f4/fff",
                Title = string.Empty,
                Description = string.Empty,
                IsFromDbContext = false
            });

            return _slideList;
        }

        public IList<SlideViewModel> GetDbContextSlider(int maxSlide)
        {
            try
            {
                return context.Courses
                                    .Where(item => item.FullPrice != item.DiscountPrice)
                                    .OrderByDescending(item => item.Rating.Average(itemRating =>itemRating.Valutation))
                                    .Take(maxSlide)
                                    .Select(item => new SlideViewModel()
                                    {
                                        UrlImage = item.ImageThumbPath,
                                        Title = item.Title,
                                        Description = item.Description.Substring(0, 50) + "..." ,
                                        IsFromDbContext = true
                                    })
                                    .ToList();
            }
            catch (Exception ex) { }

            return new List<SlideViewModel>();
        }

        public IList<SlideViewModel> GetServiceSlider(int maxSlide, int height, int width)
        {
            var _slideList = new List<SlideViewModel>();

            for (int i = 0; i < maxSlide; i++)
            {
                _slideList.Add(new SlideViewModel()
                {
                    UrlImage = string.Format(@"https://loremflickr.com/{0}/{1}/{2}", height, width, "learning"),
                    Title = string.Empty,
                    Description = string.Empty,
                    IsFromDbContext = false
                });
            }
            return _slideList;
        }
    }
}

