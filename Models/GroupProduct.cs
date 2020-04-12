using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class GroupProduct:BaseEntity
    {
        public GroupProduct()
        {

        }
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
