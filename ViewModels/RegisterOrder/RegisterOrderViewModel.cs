using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.RegisterOrder
{
  public  class RegisterOrderViewModel
    {
        public RegisterOrderViewModel()
        {

        }
        public int Id { get; set; }

        [Display(Name="نام")]
        public string Name { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name="توضیحات")]
        public string Description { get; set; }

        public bool Available { get; set; }

        public int GroupProductId { get; set; }

        public string Image_url { get; set; }
      
        [Display(Name = "تعداد")]
        public int count { get; set; }
    }
}
