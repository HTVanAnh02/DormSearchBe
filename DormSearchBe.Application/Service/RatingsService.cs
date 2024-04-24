using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Ratings;
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
    public class RatingsService: IRatingsService
    {
        private readonly IRatingsRepository _ratingRepository;
        private readonly IMapper _mapper;
        public RatingsService(IRatingsRepository ratingsRepository, IMapper mapper)
        {
            _ratingRepository = ratingsRepository;
            _mapper = mapper;
        }

        public DataResponse<RatingsList> ChangeFeedback(RatingsChangeFeedback changeFeedback)
        {
            throw new NotImplementedException();
        }

        public DataResponse<RatingsList> ChangeStatus(RatingsChangeStatus changeStatus)
        {
            throw new NotImplementedException();
        }

        public DataResponse<RatingsQuery> Create(RatingsDto dto)
        {
            dto.RatingsId = Guid.NewGuid();
            var newData = _ratingRepository.Create(_mapper.Map<Ratings>(dto));
            if (newData != null)
            {
                return new DataResponse<RatingsQuery>(_mapper.Map<RatingsQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<RatingsQuery> Delete(Guid id)
        {
            var item = _ratingRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _ratingRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<RatingsQuery>(_mapper.Map<RatingsQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<RatingsQuery> GetById(Guid id)
        {
            var item = _ratingRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<RatingsQuery>(_mapper.Map<RatingsQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<RatingsQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<RatingsQuery>>(_ratingRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.Comment.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<RatingsQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<RatingsQuery>(paginatedResult, 200, query.Count());
        }

        public PagedDataResponse<RatingsQuery> Items(CommonListQuery commonList, Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedDataResponse<RatingsList> ItemsByLandlords(CommonQueryByHome commonList, Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedDataResponse<RatingsList> ItemsByLandlordsSuitable(CommonQueryByHome commonList, Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedDataResponse<RatingsList> ItemsByStudents(CommonListQuery commonList, Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedDataResponse<RatingsList> ItemsByUser(CommonQueryByHome commonList, Guid id)
        {
            throw new NotImplementedException();
        }

        public PagedDataResponse<RatingsList> ItemsByUserSuitable(CommonQueryByHome commonList, Guid id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<List<RatingsDto>> ItemsList()
        {
            var query = _ratingRepository.GetAllData();
            if (query != null && query.Any())
            {
                var RatingsDtos = _mapper.Map<List<RatingsDto>>(query);
                return new DataResponse<List<RatingsDto>>(_mapper.Map<List<RatingsDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }


        public DataResponse<RatingsQuery> Update(RatingsDto dto)
        {
            var item = _ratingRepository.GetById(dto.RatingsId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _ratingRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<RatingsQuery>(_mapper.Map<RatingsQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
    }
}
