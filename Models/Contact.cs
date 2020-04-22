using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class Contact:BaseEntity
    {
        public Contact()
        {

        }
        public string Text { get; set; }
        public string UserName { get; set; }
    }
}
