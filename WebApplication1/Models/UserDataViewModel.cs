using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserDataViewModel
    {
        [Display(Name = "Wzrost")]
        public byte Height { get; set; }
        [Display(Name = "Waga")]
        public decimal Weight { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
    }

    public class EntriesIndexViewModel
    {
        public IEnumerable<USER_DATA> UserData { get; set; }
        public string RecentHeight { get; set; }
        public string RecentWeight { get; set; }
    }
}