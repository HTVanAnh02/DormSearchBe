using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.City
{
    public class CityQuery:BaseEntity
    {
        public Guid CityId { get; set; }
        public string? CityName { get; set; }
    }
}
