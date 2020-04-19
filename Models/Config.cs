using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Config
    {
        public Config()
        {

        }
        public int Id { get; set; }

        [Display(Name="نام")]
        public string Name { get; set; }

        [Display(Name = "کپشن")]
        public string Text { get; set; }

        [Display(Name = "مقدار")]
        public string Value { get; set; }
    }
}
