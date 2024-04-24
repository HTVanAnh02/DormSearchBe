using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Helpers
{
    public class CommonQueryByHome
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string cityId { get; set; }
        public string priceId { get; set; }
        public string areasId { get; set; }
        public string roomstyleId { get; set; }
        public string favoritesId { get; set; }
        public string keyword { get; set; }
        public CommonQueryByHome() 
        {
            page = 1;
            limit = 10;
            cityId = "";
            priceId = "";
            areasId = "";
            roomstyleId = "";
            favoritesId = "";
            keyword = "";
        }
    }
}
