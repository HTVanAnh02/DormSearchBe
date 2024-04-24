using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Roomstyle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IRoomstyleService
    {
        PagedDataResponse<RoomstyleQuery> Items(CommonListQuery commonList);
        DataResponse<List<RoomstyleDto>> ItemsList();
        DataResponse<RoomstyleQuery> Create(RoomstyleDto dto);
        DataResponse<RoomstyleQuery> Update(RoomstyleDto dto);
        DataResponse<RoomstyleQuery> Delete(Guid id);
        DataResponse<RoomstyleQuery> GetById(Guid id);
    }
}
