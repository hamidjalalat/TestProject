using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Orders
{
   public class ConfigViewModel
    {
        public ConfigViewModel()
        {

        }
        public int Id { get; set; }
        public string breadPrice { get; set; }
        public string maxorder { get; set; }
        public string maxvalue { get; set; }
        public string maxenable { get; set; }

    }
}
