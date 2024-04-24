using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IHousesService
    {
        PagedDataResponse<HousesQuery> Items(CommonListQuery commonListQuery, Guid objId);
        DataResponse<HousesQuery> Create(HousesDto dto);
        DataResponse<HousesQuery> Update(HousesDto dto);
        DataResponse<HousesQuery> Delete(Guid id);
        DataResponse<HousesQuery> GetById(Guid id);
        DataResponse<List<HousesQuery>> ItemsNoQuery();
        PagedDataResponse<HousesQuery> ItemsByHome(CommonQueryByHome queryByHome);
        DataResponse<HousesQuery> ItemById(Guid id);
        PagedDataResponse<HousesQuery> RelatedHouses(CommonQueryByHome queryByHome, Guid id);
    }
}
