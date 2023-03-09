using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataBookView.Authentification;
using DataBookView.Interfaces;

namespace DataBookView.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDataBookData _dataBookData;

        public AccountController(IDataBookData dBData)
        {
            _dataBookData = dBData;
        }

        /// <summary>
        /// Добавить токен в заголовок.
        /// </summary>
        public void AddTokenToHeader()
        {
            var token = HttpContext.Request.Cookies[".AspNetCore.Application.Id"];
            if (!string.IsNullOrEmpty(token))
                _dataBookData.AddTokenToClient(token);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "/";

            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl)) model.ReturnUrl = "/";

            if (ModelState.IsValid)
            {
                var token = _dataBookData.Login(model);

                if (token != null)
                    HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", token,
                    new CookieOptions
                    {
                        MaxAge = TimeSpan.FromMinutes(60)
                    });
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                AddTokenToHeader();

                var token = _dataBookData.Register(model);

                if (token != null)
                    HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", token,
                    new CookieOptions
                    {
                        MaxAge = TimeSpan.FromMinutes(60)
                    });
                return RedirectToAction("Index", "DataBook");
            }

            ModelState.AddModelError("", "Не корректные данные пользователя");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(".AspNetCore.Application.Id");           

            return RedirectToAction("Index", "DataBook");
        }

        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AccessDenied(UserLogin model)
        {
            return RedirectToAction("Index", "DataBook");
        }
    }
}