using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.Models.Services
{
    public interface IHeaderService
    {
        Task<IList<MenuItemViewModel>> GetMenu(bool isShowIcon, MenuItemType menuItemType);

        IList<SlideViewModel> GetDemoSlider();

        IList<SlideViewModel> GetDbContextSlider(int maxSlide);

        IList<SlideViewModel> GetServiceSlider(int maxSlide, int height, int width);
    }
}
