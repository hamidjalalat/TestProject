using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Orders
{
  public  class ProductsViewModel
    {
        public ProductsViewModel()
        {

        }
        public int RowNumber { get; set; }

        public int Id { get; set; }

        [Display(Name = "نام غذا")]
        public string Name { get; set; }

        [Display(Name = "قیمت")]
        public Int64? Price { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "موجود")]
        public bool Available { get; set; }

        [Display(Name = "تصوير", Prompt = "تصوير", Description = "تصوير")]
        public string Image_url { get; set; }

        public string ImageName { get; set; }

        public bool HasBread { get; set; }

        public int GroupProductId { get; set; }

        public bool ShowBread { get; set; }

    }
}
