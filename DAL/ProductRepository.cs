using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class ProductRepository: Repository<Models.Product>, IProductRepository
    {
        public ProductRepository(Models.DataBaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
