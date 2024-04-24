﻿using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Role;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IApprovaRepository _approvalRepository;
        public RoleService(IRoleRepository roleRepository, IMapper mapper, IApprovaRepository approvalRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _approvalRepository = approvalRepository;
        }

        public DataResponse<RoleQuery> Create(RoleDto dto)
        {
            dto.RoleId = Guid.NewGuid();
            string[] arr = dto.ApprovalId.Split(",", StringSplitOptions.RemoveEmptyEntries);
            List<string> approvalNames = new List<string>();
            foreach (var item in arr)
            {
                var permission = _approvalRepository.GetByString(item);
                if (permission == null)
                {
                    throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.NotFound);
                }
                approvalNames.Add(permission.ApprovalId);
            }

            dto.ApprovalId = string.Join(", ", approvalNames);

            var newData = _roleRepository.Create(_mapper.Map<Role>(dto));
            if (newData != null)
            {
                return new DataResponse<RoleQuery>(_mapper.Map<RoleQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);


        }

        public DataResponse<RoleQuery> Delete(Guid id)
        {
            var item = _roleRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _roleRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<RoleQuery>(_mapper.Map<RoleQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<RoleQuery> GetById(Guid id)
        {
            var item = _roleRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<RoleQuery>(_mapper.Map<RoleQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<RoleQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<RoleQuery>>(_roleRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.RoleName.Contains(commonList.keyword)).ToList();
            }

            foreach (var item in query)
            {
                string[] ApprovalIds = item.ApprovalId.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                List<string> approvalName = new List<string>();

                foreach (var ApprovalId in ApprovalIds)
                {
                    var permission = _approvalRepository.GetByString(ApprovalId);

                    if (permission != null)
                    {
                        approvalName.Add(permission.ApprovalName);
                    }
                }

                item.ApprovalName = string.Join(", ", approvalName);
            }


            var paginatedResult = PaginatedList<RoleQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<RoleQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<RoleQuery> Update(RoleDto dto)
        {
            var item = _roleRepository.GetById(dto.RoleId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            string[] arr = dto.ApprovalId.Split(",", StringSplitOptions.RemoveEmptyEntries);
            List<string> approvalName = new List<string>();
            foreach (var items in arr)
            {
                var permission = _approvalRepository.GetByString(items);
                if (permission == null)
                {
                    throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.NotFound);
                }
                approvalName.Add(permission.ApprovalId);
            }
            dto.ApprovalId = string.Join(", ", approvalName);
            var newData = _roleRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<RoleQuery>(_mapper.Map<RoleQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
    }
}
