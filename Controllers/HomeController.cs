using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QTEMP.Models;

namespace QTEMP.Controllers
{
    public class HomeController : Controller
    {
        private readonly MemoryCacheEntryOptions _temporaryOptions = new MemoryCacheEntryOptions();
        private readonly MemoryCacheEntryOptions _permanentOptions = new MemoryCacheEntryOptions();
        private readonly IMemoryCache _cache;
        public HomeController(IMemoryCache cache)
        {
            _temporaryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            _temporaryOptions.SlidingExpiration = TimeSpan.FromMinutes(30);
            _permanentOptions.SetPriority(CacheItemPriority.NeverRemove);
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _cache.Set("isLogin", false, _permanentOptions);
            return Json(new { isSuccess = true } as dynamic);
        }
        
        public IActionResult CheckLogin()
        {
            if (!_cache.Get<bool>("isLogin"))
            {
                _cache.Set("isLogin", false, _permanentOptions);
            }
            var current = new { isLogin = _cache.Get<bool>("isLogin") } as dynamic;
            return Json(current);
        }
        
        public IActionResult VerifyLogin(string pinCode)
        {
            if (pinCode != "lexroot") return Json(new { isSuccess = false } as dynamic);
            _cache.Set("isLogin", true, _permanentOptions);
            return Json(new { isSuccess = true} as dynamic);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}