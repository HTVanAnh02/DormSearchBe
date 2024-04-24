using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Ratings
{
    public class RatingsDto
    {
        public Guid RatingsId { get; set; }
        public Guid? StudentsId { get; set; }
        public Guid? HousesId { get; set; }
        public Guid? LandlordsId { get; set; }
        public string? RatingsDateTime { get; set; }
        public bool? IsStatus { get; set; }
        public string? Content { get; set; }
    }
}
