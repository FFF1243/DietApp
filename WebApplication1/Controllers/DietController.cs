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
    [Authorize]
    public class DietController : Controller
    {
        private DietService _dietService;
        private ProductService _productService;

        public DietController()
        {
            _dietService = new DietService();
            _productService = new ProductService();
        }

        public ActionResult Index()
        {
            var vm = _dietService.GetAllUsersDiets(User.Identity.GetUserName());
            return View(vm);
        }

        //GET
        [HttpGet]
        public ActionResult AddProducts(int id = 1)
        {
            var allProducts = _productService.GetSelectAllProductsListItem();
            var vm = new ProductDietViewModel()
            {
                AllProducts = allProducts,
                DietId = id
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddProducts(ProductDietViewModel vm)
        {
            _dietService.AddProductToDiet(vm);

            return RedirectToAction("Create", new { dietId = vm.DietId });
        }


        public ActionResult CreateNewDiet()
        {
            var id = _dietService.AddDiet(DateTime.Now);
            return RedirectToAction("Create", new { dietId = id });
        }

        //CREATE DIET
        [HttpGet]
        public ActionResult Create(int dietId)
        {
            var allProducts = _productService.GetSelectAllProductsListItem();
            var productVm = new ProductDietViewModel()
            {
                AllProducts = allProducts,
                DietId = dietId
            };
            var vm = new DietViewModel()
            {
                DietId = dietId,
                ChosenProducts = _dietService.GetChosenProducts(dietId),
                ProductDietViewModel = productVm,
                Date = DateTime.Now.Date
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(DietViewModel vm)
        { 
            _dietService.RegisterDietForUser(vm, User.Identity.Name);
            _dietService.UpdateDate(vm.DietId, vm.Date);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int dietId)
        {
            _dietService.DeleteDiet(dietId);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return RedirectToAction("Create", new { dietId = id });
        }

        [HttpGet]
        public ActionResult Share(int id)
        {
            var vm = new ShareDietViewModel()
            {
                DietId = id
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Share(ShareDietViewModel vm)
        {
            _dietService.RegisterDietForUser(vm);
            return RedirectToAction("Index");
        }

    }
}