using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class FactorDetail
    {
        public FactorDetail()
        {

        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
   
        public Int64 Price { get; set; }
        public int Count { get; set; }
        public Guid FactorId { get; set; }
        public virtual Factor Factor { get; set; }
    }
}
