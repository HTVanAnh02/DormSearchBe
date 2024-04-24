using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Ratings
{
    public class RatingsChangeFeedback
    {
        public Guid RatingsId {  get; set; }    
        public bool IsFeedback {  get; set; }
    }
}
