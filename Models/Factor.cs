using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class Factor
    {
        public Factor()
        {

        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public virtual List<FactorDetail> FactorDetails { get; set; }

    }
}
