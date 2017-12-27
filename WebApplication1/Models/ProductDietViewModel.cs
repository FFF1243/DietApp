using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class ProductDietViewModel
    {
        public IEnumerable<SelectListItem> AllProducts { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int DietId { get; set; }

        [Required]
        [Display(Name = "Waga [g]")]

        public int Quantity { get; set; }
    }


}