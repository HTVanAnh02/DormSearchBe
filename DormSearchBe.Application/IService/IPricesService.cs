using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Prices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IPricesService
    {
        PagedDataResponse<PricesQuery> Items(CommonListQuery commonList);
        DataResponse<List<PricesDto>> ItemsList();
        DataResponse<PricesQuery> Create(PricesDto dto);
        DataResponse<PricesQuery> Update(PricesDto dto);
        DataResponse<PricesQuery> Delete(Guid id);
        DataResponse<PricesQuery> GetById(Guid id);
    }
}
