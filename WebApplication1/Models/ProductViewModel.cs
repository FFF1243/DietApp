using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<PRODUCT> Products { get; set; }
    }

    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Nazwa / Opis Produktu")]
        public string Description { get; set; }

        [Display(Name = "Kalorie")]
        public byte Kcal { get; set; }
        [Display(Name = "Białko")]
        public byte Protein { get; set; }
        [Display(Name = "Węglowodany")]
        public byte Carbs { get; set; }
        [Display(Name = "Tłuszcze")]
        public byte Fat { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}