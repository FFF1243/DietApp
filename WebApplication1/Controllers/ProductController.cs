using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories.Concrete;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
      //  private IProductRepository repo = new ProductRepository();
        private ProductService _service;
        public ProductController()
        {
            _service = new ProductService();
        }

        [HttpGet]
        public ViewResult Index()
        {
            var vm = _service.GetAllProducts();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ProductViewModel vm)
        {
             int id = _service.AddProduct(vm);
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (vm.ImageUpload != null && vm.ImageUpload.ContentLength > 0)
            {
                var uploadDir = "~/Content/Images/Products";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), $"{id}.jpg");
                var imageUrl = Path.Combine(uploadDir, vm.ImageUpload.FileName);
                vm.ImageUpload.SaveAs(imagePath);
            }


            return RedirectToAction("Index");
        }

        public PartialViewResult PartialIndex()
        {
            var vm = _service.GetAllProducts();
            return PartialView(vm);
        }

    }
}