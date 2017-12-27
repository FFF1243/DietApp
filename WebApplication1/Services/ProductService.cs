using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories.Concrete;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Services
{
    public class ProductService
    {
        private IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public ProductListViewModel GetAllProducts()
        {
            ProductListViewModel vm = new ProductListViewModel()
            {
                Products = _productRepository.Products
            };
            return vm;
        }

        public int AddProduct(ProductViewModel vm)
        {
            var product = new PRODUCT()
            {
                PRODUCT_DESCRIPTION = vm.Description,
                CARBS_100 = vm.Carbs,
                KCAL_100 = vm.Kcal,
                PROTEIN_100 = vm.Protein,
                FAT_100 = vm.Fat
            };



            _productRepository.AddProductToDb(product);

            var id =
                _productRepository.Products.FirstOrDefault(x => x.PRODUCT_DESCRIPTION == product.PRODUCT_DESCRIPTION).PRODUCT_ID;

            return id;
        }

        public IEnumerable<SelectListItem> GetSelectAllProductsListItem()
        {
            var result = new SelectList(_productRepository.Products, "PRODUCT_ID", "PRODUCT_DESCRIPTION");
            return result;
        }
    }
}