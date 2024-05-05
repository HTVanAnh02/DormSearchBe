using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IAreasService
    {
        PagedDataResponse<AreasQuery> Items(CommonListQuery commonList);
        DataResponse<List<AreasDto>> ItemsList();
        DataResponse<AreasQuery> Create(AreasDto dto);
        DataResponse<AreasQuery> Update(AreasDto dto);
        DataResponse<AreasQuery> Delete(Guid id);
        DataResponse<AreasQuery> GetById(Guid id);
       
    }
}
