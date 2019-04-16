using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Models.Services;

namespace TalkAspNetCoreDemo.Controllers.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IHeaderService service;

        public MenuViewComponent(IHeaderService service)
        {
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isShowIcon, MenuItemType menuItemType)
        {
            return View(await this.service.GetMenu(isShowIcon, menuItemType));
        }
    }
}
