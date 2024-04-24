using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Prices
{
    public class PricesQuery:BaseEntity
    {
        public Guid PriceId { get; set; }
        public string? Price { get; set; }
    }
}
