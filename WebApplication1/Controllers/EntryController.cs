using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;
using WebApplication1.Repositories.Concrete;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class EntryController : Controller
    {
        // GET: Entry
        //private UserRepostiory userRepo = new UserRepostiory();
        private EntryService _service;
        public EntryController()
        {
            _service = new EntryService();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var vm = _service.GetEntriesVM(User.Identity.GetUserName());
            
            return View(vm);
        }

        //[HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
           _service.DeleteUserData(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(UserDataViewModel vm)
        {
            _service.CreateUserEntry(User.Identity.GetUserName(),vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Chart()
        {
            var model = _service.GetChartForUser(User.Identity.GetUserName());
            return View(model);
        }
    }
}