using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Roomstyle;
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
    public class RoomstyleService :IRoomstyleService
    {
        private readonly IRoomstyleRepository _roomstyleRepository;
        private readonly IMapper _mapper;

        public RoomstyleService(IRoomstyleRepository roomstyleRepository, IMapper mapper)
        {
            _roomstyleRepository = roomstyleRepository;
            _mapper = mapper;
        }

        public DataResponse<RoomstyleQuery> Create(RoomstyleDto dto)
        {
            dto.RoomstyleId = Guid.NewGuid();
            var newData = _roomstyleRepository.Create(_mapper.Map<Roomstyle>(dto));
            if (newData != null)
            {
                return new DataResponse<RoomstyleQuery>(_mapper.Map<RoomstyleQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<RoomstyleQuery> Delete(Guid id)
        {
            var item = _roomstyleRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _roomstyleRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<RoomstyleQuery>(_mapper.Map<RoomstyleQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<RoomstyleQuery> GetById(Guid id)
        {
            var item = _roomstyleRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<RoomstyleQuery>(_mapper.Map<RoomstyleQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<RoomstyleQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<RoomstyleQuery>>(_roomstyleRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.RoomstyleName.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<RoomstyleQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<RoomstyleQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<RoomstyleDto>> ItemsList()
        {
            var query = _roomstyleRepository.GetAllData();
            if (query != null && query.Any())
            {
                var RoomstyleDtos = _mapper.Map<List<RoomstyleDto>>(query);
                return new DataResponse<List<RoomstyleDto>>(_mapper.Map<List<RoomstyleDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }


        public DataResponse<RoomstyleQuery> Update(RoomstyleDto dto)
        {
            var item = _roomstyleRepository.GetById(dto.RoomstyleId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _roomstyleRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<RoomstyleQuery>(_mapper.Map<RoomstyleQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
    }
}
