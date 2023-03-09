using Microsoft.AspNetCore.Mvc;
using DataBookView.Models;
using DataBookView.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DataBookView.Controllers
{
    public class DataBookController : Controller
    {
        private readonly IDataBookData _dataBookData;
        public DataBookController(IDataBookData dBData)
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
        public IActionResult Index()
        {
            AddTokenToHeader();
            ViewBag.DataBook = _dataBookData.GetAllDatabooks();
            return View();
        }

        [HttpGet]
        public IActionResult GetDataBook(int dataBookId)
        {
            AddTokenToHeader();
            return View(_dataBookData.ReadDataBook(dataBookId));
        }

        [HttpGet]
        public IActionResult AddDataBook()
        {
            AddTokenToHeader();
            return View();
        }

        [HttpPost]
        public IActionResult AddDataFromField(DataBook dataBook)
        {
            AddTokenToHeader();
            _dataBookData.CreateDataBook(dataBook);
            return Redirect("~/");
        }

        [HttpPost]
        public IActionResult EditDataBook(int dataBookId)
        {
            AddTokenToHeader();
            return View(_dataBookData.ReadDataBook(dataBookId));
        }

        [HttpPost]
        public IActionResult EditDataFromField(DataBook dataBook)
        {
            AddTokenToHeader();
            _dataBookData.UpdateDataBook(dataBook);
            return Redirect("~/");
        }

        [HttpPost]
        public IActionResult DeleteDataBook(int dataBookId)
        {
            AddTokenToHeader();
            return View(_dataBookData.ReadDataBook(dataBookId));
        }

        [HttpPost]
        public IActionResult DeleteDataFromField(DataBook dataBook)
        {
            AddTokenToHeader();
            _dataBookData.DeleteDataBook(dataBook);
            return Redirect("~/");
        }
    }
}