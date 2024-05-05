using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Areas;
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
    public class AreasService : IAreasService
    {
        private readonly IAreasRepository _areasRepository;
        private readonly IMapper _mapper;

        public AreasService(IAreasRepository areasRepository, IMapper mapper)
        {
            _areasRepository = areasRepository;
            _mapper = mapper;
        }

        public DataResponse<AreasQuery> Create(AreasDto dto)
        {
            dto.AreasId = Guid.NewGuid();
            var newData = _areasRepository.Create(_mapper.Map<Areas>(dto));
            if (newData != null)
            {
                return new DataResponse<AreasQuery>(_mapper.Map<AreasQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<AreasQuery> Delete(Guid id)
        {
            var item = _areasRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _areasRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<AreasQuery>(_mapper.Map<AreasQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<AreasQuery> GetById(Guid id)
        {
            var item = _areasRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<AreasQuery>(_mapper.Map<AreasQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<AreasQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<AreasQuery>>(_areasRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.AreasName.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<AreasQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<AreasQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<AreasDto>> ItemsList()
        {
            var query = _areasRepository.GetAllData();
            if (query != null && query.Any())
            {
                var AreasDtos = _mapper.Map<List<AreasDto>>(query);
                return new DataResponse<List<AreasDto>>(_mapper.Map<List<AreasDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }


        public DataResponse<AreasQuery> Update(AreasDto dto)
        {
            var item = _areasRepository.GetById(dto.AreasId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _areasRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<AreasQuery>(_mapper.Map<AreasQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
    }
}
