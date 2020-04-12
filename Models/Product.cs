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
        [Required(AllowEmptyStrings =false,ErrorMessage = "لطفا نام غذا را وارد نمایید")]
        public string Name { get; set; }
        [Display(Name = "قیمت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفاقیمت را وارد نمایید")]
        public Int64 Price { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "موجود")]
        public bool Available { get; set; }
        [Display(Name = "تصوير")]
        public string Image_url { get; set; }
        public string ImageName { get; set; }
        public int GroupProductId { get; set; }
        public virtual GroupProduct GroupProduct { get; set; }

    }
}
