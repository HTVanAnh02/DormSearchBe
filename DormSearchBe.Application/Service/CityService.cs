﻿using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.City;
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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public DataResponse<CityQuery> Create(CityDto dto)
        {
            dto.CityId = Guid.NewGuid();
            var newData = _cityRepository.Create(_mapper.Map<City>(dto));
            if (newData != null)
            {
                return new DataResponse<CityQuery>(_mapper.Map<CityQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<CityQuery> Delete(Guid id)
        {
            var item = _cityRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _cityRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<CityQuery>(_mapper.Map<CityQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<CityQuery> FindCityByEmail(Guid id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<CityQuery> FindCityByName(string name)
        {
            throw new NotImplementedException();
        }

        public DataResponse<CityQuery> GetById(Guid id)
        {
            var item = _cityRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<CityQuery>(_mapper.Map<CityQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public DataResponse<CityQuery> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        /*public DataResponse<CityQuery> GetByName(string name)
        {
            throw new NotImplementedException();
        }*/

        public PagedDataResponse<CityQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<CityQuery>>(_cityRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.CityName.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<CityQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<CityQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<CityDto>> ItemsList()
        {
            var query = _cityRepository.GetAllData();
            if (query != null && query.Any())
            {
                var cityDtos = _mapper.Map<List<CityDto>>(query);
                return new DataResponse<List<CityDto>>(_mapper.Map<List<CityDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }
        public DataResponse<CityQuery> Update(CityDto dto)
        {
            var item = _cityRepository.GetById(dto.CityId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _cityRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<CityQuery>(_mapper.Map<CityQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
       /* public DataResponse<CityQuery> GetByName(string name)
        {
            var city = _cityRepository.GetByName(name);
            if (city == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }

            return new DataResponse<CityQuery>(_mapper.Map<CityQuery>(city), HttpStatusCode.OK, HttpStatusMessages.OK);
        }*/


    }
}
