using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Permission;
using DormSearchBe.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IPermissionService
    {
        PagedDataResponse<PermissionQuery> Items(CommonListQuery commonListQuery);
        DataResponse<PermissionQuery> Create(PermissionDto dto);
        DataResponse<PermissionQuery> Update(PermissionDto dto);
        DataResponse<PermissionQuery> Delete(string id);
    }
}
