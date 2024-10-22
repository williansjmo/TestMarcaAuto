using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Entities
{
    public class CarBrands : BaseEntity<Guid>
    {
        public string Brand { get; set; }
    }
}
