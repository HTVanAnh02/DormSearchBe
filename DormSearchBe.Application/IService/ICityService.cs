using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface ICityService
    {
        PagedDataResponse<CityQuery> Items(CommonListQuery commonList);
        DataResponse<List<CityDto>> ItemsList();
        DataResponse<CityQuery> Create(CityDto dto);
        DataResponse<CityQuery> Update(CityDto dto);
        DataResponse<CityQuery> Delete(Guid id);
        DataResponse<CityQuery> GetById(Guid id);
    }
    
}
