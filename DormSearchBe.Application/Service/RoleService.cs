using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Role;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Repositories;
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
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public DataResponse<RoleQuery> Create(RoleDto dto)
        {
            dto.RoleId = Guid.NewGuid();
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

        public IEnumerable<RoleDto> getALL_NoQuey()
        {
            throw new NotImplementedException();
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

        public RoleDto getRoleById(Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedDataResponse<RoleQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<RoleQuery>>(_roleRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.RoleName.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<RoleQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<RoleQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<RoleDto>> ItemsList()
        {
            var query = _roleRepository.GetAllData();
            if (query != null && query.Any())
            {
                var cityDtos = _mapper.Map<List<RoleDto>>(query);
                return new DataResponse<List<RoleDto>>(_mapper.Map<List<RoleDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }
        public DataResponse<RoleQuery> Update(RoleDto dto)
        {
            var item = _roleRepository.GetById(dto.RoleId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _roleRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<RoleQuery>(_mapper.Map<RoleQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
        /*      public DataResponse<RoleQuery> GetByName(string name)
              {
                  var city = _roleRepository.GetByName(name);
                  if (city == null)
                  {
                      throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
                  }

                  return new DataResponse<RoleQuery>(_mapper.Map<RoleQuery>(city), HttpStatusCode.OK, HttpStatusMessages.OK);
              }*/


    }
}
