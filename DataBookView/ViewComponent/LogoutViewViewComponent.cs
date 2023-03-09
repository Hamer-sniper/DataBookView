using Microsoft.AspNetCore.Mvc;

namespace DataBookView.Component
{
    public class LogoutViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
