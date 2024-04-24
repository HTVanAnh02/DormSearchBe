using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IApprovalService
    {
        PagedDataResponse<ApprovalQuery> Items(CommonListQuery commonList);
        DataResponse<ApprovalQuery> Create(ApprovalDto dto);
        DataResponse<ApprovalQuery> Update(ApprovalDto dto);
        DataResponse<ApprovalQuery> Delete(string id);
    }
}
