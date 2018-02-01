using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<PRODUCT> Products
        {
            get
            {
                using (var context = new DietDbContext())
                {
                    return context.PRODUCTS.ToList();
                }
            }
        }

        public void AddProductToDb(PRODUCT product)
        {
            using (var ctx = new DietDbContext())
            {
                ctx.CREATEPRODUCT(product.PRODUCT_DESCRIPTION, product.KCAL_100, product.PROTEIN_100, product.FAT_100,
                    product.CARBS_100);
                ctx.SaveChanges();
            }
        }

    }
}