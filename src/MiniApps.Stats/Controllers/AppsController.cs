using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApps.Stats.ViewModels.AppsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Controllers
{
    [Authorize]
    public class AppsController : Controller
    {
        public AppsController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> UserLoginCount()
        {
            var model = new UserLoginCountViewModel();
            
            
            return View();
        }       
        
        [HttpGet]
        public async Task<IActionResult> UserActiveCount()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> NewUserCount()
        {
            throw new NotImplementedException();
        }
    }
}
