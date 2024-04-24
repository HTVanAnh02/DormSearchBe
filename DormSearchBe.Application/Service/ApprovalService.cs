using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Approval;
using DormSearchBe.Domain.Dto.Areas;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Service
{
    public class ApprovalService: IApprovalService
    {
        private readonly IApprovaRepository _approvalRepository;
        private readonly IMapper _mapper;
        public ApprovalService(IApprovaRepository approvalRepository, IMapper mapper)
        {
            _approvalRepository = approvalRepository;
            _mapper = mapper;
        }

        public DataResponse<ApprovalQuery> Create(ApprovalDto dto)
        {
            dto.ApprovalId = Guid.NewGuid().ToString();
            var newData = _approvalRepository.Create(_mapper.Map<Approval>(dto));
            if (newData != null)
            {
                return new DataResponse<ApprovalQuery>(_mapper.Map<ApprovalQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<ApprovalQuery> Delete(string id)
        {
            var item = _approvalRepository.GetByString(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _approvalRepository.DeleteByString(id);
            if (data != null)
            {
                return new DataResponse<ApprovalQuery>(_mapper.Map<ApprovalQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);

        }

        public PagedDataResponse<ApprovalQuery> Items(CommonListQuery commonListQuery)
        {
            var query = _mapper.Map<List<ApprovalQuery>>(_approvalRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonListQuery.keyword))
            {
                query = query.Where(x => x.AppApprovalName.Contains(commonListQuery.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<ApprovalQuery>.ToPageList(query, commonListQuery.page, commonListQuery.limit);
            return new PagedDataResponse<ApprovalQuery>(paginatedResult, 200, query.Count());

        }

        public DataResponse<ApprovalQuery> Update(ApprovalDto dto)
        {
            var item = _approvalRepository.GetByString(dto.ApprovalId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _approvalRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<ApprovalQuery>(_mapper.Map<ApprovalQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);

        }
    }
}
