using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DataBookView.Roles;
using DataBookView.Authentification;
using Microsoft.AspNetCore.Authorization;
using DataBookView.Interfaces;
using System.Xml.Linq;

namespace DataBookView.Controllers
{
    public class RolesController : Controller
    {
        private readonly IDataBookData _dataBookData;

        public RolesController(IDataBookData dBData)
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
        public IActionResult Index()
        {
            AddTokenToHeader();

            return View(_dataBookData.GetRoles());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            AddTokenToHeader();

            if (!string.IsNullOrEmpty(name))
                _dataBookData.CreateRole(name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            AddTokenToHeader();

            if (!string.IsNullOrEmpty(name))
                _dataBookData.DeleteRole(name);

            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            AddTokenToHeader();

            return View(_dataBookData.UserList());
        }

        public async Task<IActionResult> DeleteUser(string userName)
        {
            AddTokenToHeader();

            if (!string.IsNullOrEmpty(userName))
                _dataBookData.DeleteUser(userName);

            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> GetUserAndRoles(string userName)
        {
            AddTokenToHeader();

            return View(_dataBookData.GetUserAndRoles(userName));
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAndRoles(string userName, List<string> roles)
        {
            AddTokenToHeader();

            var result = _dataBookData.EditUserAndRoles(userName, roles);

            return RedirectToAction("UserList");
        }

    }
}
