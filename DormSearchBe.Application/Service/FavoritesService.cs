using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Favorites;
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
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IMapper _mapper;
        public FavoritesService(IFavoritesRepository favoritesRepository, IMapper mapper)
        {
            _favoritesRepository = favoritesRepository;
            _mapper = mapper;
        }

        public DataResponse<FavoritesQuery> Create(FavoritesDto dto)
        {
            dto.FavoritesId = Guid.NewGuid();
            var newData = _favoritesRepository.Create(_mapper.Map<Favorites>(dto));
            if (newData != null)
            {
                return new DataResponse<FavoritesQuery>(_mapper.Map<FavoritesQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<FavoritesQuery> Delete(Guid id)
        {
            var item = _favoritesRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _favoritesRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<FavoritesQuery>(_mapper.Map<FavoritesQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<FavoritesQuery> GetById(Guid id)
        {
            var item = _favoritesRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<FavoritesQuery>(_mapper.Map<FavoritesQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<FavoritesQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<FavoritesQuery>>(_favoritesRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.FavoritesName.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<FavoritesQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<FavoritesQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<FavoritesDto>> ItemsList()
        {
            var query = _favoritesRepository.GetAllData();
            if (query != null && query.Any())
            {
                var FavoritesDtos = _mapper.Map<List<FavoritesDto>>(query);
                return new DataResponse<List<FavoritesDto>>(_mapper.Map<List<FavoritesDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }


        public DataResponse<FavoritesQuery> Update(FavoritesDto dto)
        {
            var item = _favoritesRepository.GetById(dto.FavoritesId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _favoritesRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<FavoritesQuery>(_mapper.Map<FavoritesQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
    }
}    