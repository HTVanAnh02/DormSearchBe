using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IRoleService
    {
      
         PagedDataResponse<RoleQuery> Items(CommonListQuery commonList);
         DataResponse<RoleQuery> Create(RoleDto dto);
         DataResponse<RoleQuery> Update(RoleDto dto);
         DataResponse<RoleQuery> Delete(Guid id);
         DataResponse<RoleQuery> GetById(Guid id);
         IEnumerable<RoleDto> getALL_NoQuey();
         RoleDto getRoleById(Guid id);
    }
}
