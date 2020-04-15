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
            Id = Guid.NewGuid();

        }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string   UserName { get; set; }
        public virtual List<FactorDetail> FactorDetails { get; set; }

    }
}
