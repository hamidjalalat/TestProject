using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Product:BaseEntity
    {
        public Product()
        {

        }
        [Display( Name = "نام غذا")]
        public string Name { get; set; }
        [Display(Name = "قیمت")]
        public Int64 Price { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "موجود")]
        public bool Available { get; set; }
        [Display(Name = "تصوير", Prompt = "تصوير", Description = "تصوير")]
        public string Image_url { get; set; }
        public string ImageName { get; set; }
    }
}
