using DataBookView.Interfaces;
using DataBookView.Roles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Xml.Linq;

namespace DataBookView.Component
{
    public class LogoutViewViewComponent : ViewComponent
    {
        private readonly IDataBookData _dataBookData;

        public LogoutViewViewComponent(IDataBookData dBData)
        {
            _dataBookData = dBData;
        }

        public IViewComponentResult Invoke()
        {
            ChangeRole currentUser = null;

            var token = HttpContext.Request.Cookies[".AspNetCore.Application.Id"];
            if (!string.IsNullOrEmpty(token))
            {
                _dataBookData.AddTokenToClient(token);
                currentUser = _dataBookData.GetCurrentUser();
            }
            
            return View(currentUser);
        }
    }

}
