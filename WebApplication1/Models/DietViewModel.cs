using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DietViewModel
    {
        public int DietId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Produkty")]
        public Dictionary<PRODUCT, short> ChosenProducts { get; set; }
        public ProductDietViewModel ProductDietViewModel { get; set; }
    }


    public class ShareDietViewModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }

        public int DietId { get; set; }
    }

}