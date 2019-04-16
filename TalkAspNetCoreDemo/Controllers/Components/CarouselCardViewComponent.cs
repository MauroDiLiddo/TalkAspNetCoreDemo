using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Models.Enums;
using TalkAspNetCoreDemo.Models.Services;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.Controllers.Components
{
    public class CarouselCardViewComponent : ViewComponent
    {
        private readonly IHeaderService service;

        public CarouselCardViewComponent(IHeaderService service)
        {
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxSlide, DataSourceType sourceType)
        {
            IList<SlideViewModel> _slideList;
            switch (sourceType)
            {
                case DataSourceType.Demo:
                    _slideList = service.GetDemoSlider();
                    break;
                case DataSourceType.DbContext:
                  _slideList=  service.GetDbContextSlider(maxSlide);
                    break;
                case DataSourceType.FromService:
                    _slideList = service.GetServiceSlider(maxSlide, 600,800);
                    break;
                default:
                    _slideList = service.GetDemoSlider();
                    break;
            }

            return View(_slideList);
        }
    }
}
