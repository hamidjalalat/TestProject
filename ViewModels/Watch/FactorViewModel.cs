using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Watch
{
    public class FactorViewModel
    {
        public FactorViewModel()
        {

        }
        public int RowNumber { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string approved { get; set; }
        public string Date { get; set; }
    }
}
