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
        public bool approved { get; set; }
        public string   Address { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public virtual List<FactorDetail> FactorDetails { get; set; }

    }
}
