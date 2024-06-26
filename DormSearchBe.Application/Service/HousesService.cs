﻿using AutoMapper;
using CloudinaryDotNet;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Areas;
using DormSearchBe.Domain.Dto.City;
using DormSearchBe.Domain.Dto.Houses;
using DormSearchBe.Domain.Dto.Prices;
using DormSearchBe.Domain.Dto.Roomstyle;
using DormSearchBe.Domain.Dto.User;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Service
{
    public class HousesService : IHousesService
    {
        private readonly IHousesRepository _housesRepository;
        private readonly IMapper _mapper;
        private readonly IAreasRepository _areasRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPricesRepository _pricesRepository;
        private readonly IRoomstyleRepository _roomstyleRepository;
        private readonly IUserRepository _usersRepository;
        private readonly Cloudinary _cloudinary;
        public HousesService(IHousesRepository housesRepository, IMapper mapper,
            IAreasRepository areasRepository,
            ICityRepository cityRepository,
            IPricesRepository aryRepository,
            IRoomstyleRepository roomstyleRepository,
            IUserRepository usersRepository,
            Cloudinary cloudinary)
        {
            _housesRepository = housesRepository;
            _mapper = mapper;
            _areasRepository = areasRepository;
            _cityRepository = cityRepository;
            _pricesRepository = aryRepository;
            _roomstyleRepository = roomstyleRepository;
            _usersRepository = usersRepository;
            _cloudinary = cloudinary;
        }
        public DataResponse<HousesQuery> Create(HousesDto dto)
        {
            UpLoadImage upload = new UpLoadImage(_cloudinary);
            dto.HousesId = Guid.NewGuid();
            if (dto.file != null)
            {
                dto.Photos = upload.ImageUpload(dto.file);
            }
            var newData = _housesRepository.Create(_mapper.Map<Houses>(dto));
            if (newData != null)
            {
                return new DataResponse<HousesQuery>(_mapper.Map<HousesQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }
        public DataResponse<HousesQuery> Update(HousesDto dto)
        {
            UpLoadImage upload = new UpLoadImage(_cloudinary);
            var item = _housesRepository.GetById(dto.HousesId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            if (dto.imageDelete != null)
            {
                upload.DeleteImage(dto.imageDelete);
            }
            if (dto.file != null)
            {
                if (item.Photos != null)
                {
                    upload.DeleteImage(item.Photos);
                }
                dto.Photos = upload.ImageUpload(dto.file);
            }
            var newData = _housesRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<HousesQuery>(_mapper.Map<HousesQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
        public DataResponse<HousesQuery> Delete(Guid id)
        {
            UpLoadImage upload = new UpLoadImage(_cloudinary);

            var item = _housesRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            if (item.Photos != null)
            {
                upload.DeleteImage(item.Photos);
            }
            var data = _housesRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<HousesQuery>(_mapper.Map<HousesQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }


        public PagedDataResponse<HousesQuery> Items(CommonListQuery commonListQuery, Guid objId)
        {
            var query = _mapper.Map<List<HousesQuery>>(_housesRepository.GetAllData().Where(x => x.UserId == objId).ToList());
            var areas = _mapper.Map<List<AreasDto>>(_areasRepository.GetAllData());
            var pricess = _mapper.Map<List<PricesDto>>(_pricesRepository.GetAllData());
            var cities = _mapper.Map<List<CityDto>>(_cityRepository.GetAllData());
            var users = _mapper.Map<List<UserDto>>(_usersRepository.GetAllData());
            var roomstyles = _mapper.Map<List<RoomstyleDto>>(_roomstyleRepository.GetAllData());
            var items = from houses in query
                        join area in areas on houses.AreasId equals area.AreasId
                        join prices in pricess on houses.PriceId equals prices.PriceId
                        join roomstyle in roomstyles on houses.RoomstyleId equals roomstyle.RoomstyleId
                        join user in users on houses.UsersId equals user.UserId
                        join city in cities on houses.CityId equals city.CityId

                        select new HousesQuery
                        {
                            HousesId = houses.HousesId,
                            HousesName = houses.HousesName,
                            Title = houses.Title,
                            Interior = houses.Interior,
                            AddressHouses = houses.AddressHouses,
                            DateSubmitted = houses.DateSubmitted,
                            AreasName = area.AreasName,
                            Price = prices.Price,
                            RoomstyleName = roomstyle.RoomstyleName,
                            UsersName = user.FullName,
                            CityName = city.CityName,
                        };
            if (!string.IsNullOrEmpty(commonListQuery.keyword))
            {
                items = items.Where(x => x.CityName.Contains(commonListQuery.keyword) ||
                 x.HousesName.Contains(commonListQuery.keyword) ||
                 x.Title.Contains(commonListQuery.keyword) ||
                 x.Interior.Contains(commonListQuery.keyword) ||
                 x.AddressHouses.Contains(commonListQuery.keyword) ||
                 x.AreasName.Contains(commonListQuery.keyword) ||
                 x.Price.Contains(commonListQuery.keyword) ||
                 x.CityName.Contains(commonListQuery.keyword) ||
                 x.UsersName.Contains(commonListQuery.keyword) ||
                 x.RoomstyleName.Contains(commonListQuery.keyword)).ToList();
            }

            var paginatedResult = PaginatedList<HousesQuery>.ToPageList(_mapper.Map<List<HousesQuery>>(items), commonListQuery.page, commonListQuery.limit);
            return new PagedDataResponse<HousesQuery>(paginatedResult, 200, items.Count());
        }


        public DataResponse<HousesQuery> GetById(Guid id)
        {
            var item = _housesRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<HousesQuery>(_mapper.Map<HousesQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }
        public DataResponse<List<HousesQuery>> ItemsNoQuery()
        {
            var query = _housesRepository.GetAllData();
            if (query != null && query.Any())
            {
                var cityDtos = _mapper.Map<List<UserDto>>(query);
                return new DataResponse<List<HousesQuery>>(_mapper.Map<List<HousesQuery>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }
        public PagedDataResponse<HousesQuery> ItemsByHome(CommonQueryByHome queryByHome)
        {
            var query = _housesRepository.GetAllData().AsQueryable();
            var pricess = _pricesRepository.GetAllData();
            var areas = _areasRepository.GetAllData();
            var roomstyles = _roomstyleRepository.GetAllData();
            var cities = _cityRepository.GetAllData();
            var users = _usersRepository.GetAllData();

            var items = from houses in query
                        join area in areas on houses.AreasId equals area.AreasId
                        join roomstyle in roomstyles on houses.RoomstyleId equals roomstyle.RoomstyleId
                        join user in users on houses.UserId equals user.UserId
                        join city in cities on houses.CityId equals city.CityId
                        select new HousesQuery
                        {
                            HousesId = houses.HousesId,
                            HousesName = houses.HousesName,
                            Title = houses.Title,
                            Interior = houses.Interior,
                            AddressHouses = houses.AddressHouses,
                            DateSubmitted = houses.DateSubmitted,
                            AreasId = houses.AreasId,
                            AreasName = area.AreasName,
                            RoomstyleId = houses.RoomstyleId,
                            RoomstyleName = roomstyle.RoomstyleName,
                            UsersId = houses.UserId,
                            UsersName = user.FullName,
                            CityId = houses.CityId,
                            CityName = city.CityName,
                        };

            if (!string.IsNullOrEmpty(queryByHome.keyword))
            {
                items = items.Where(x =>
                                         x.HousesName.Contains(queryByHome.keyword) ||
                                         x.Title.Contains(queryByHome.keyword) ||
                                         x.Interior.Contains(queryByHome.keyword) ||
                                         x.AddressHouses.Contains(queryByHome.keyword) ||
                                         x.AreasName.Contains(queryByHome.keyword) ||
                                         x.Price.Contains(queryByHome.keyword) ||
                                         x.CityName.Contains(queryByHome.keyword) ||
                                         x.UsersName.Contains(queryByHome.keyword) ||
                                         x.RoomstyleName.Contains(queryByHome.keyword));
            }

            if (!string.IsNullOrEmpty(queryByHome.areasId) && Guid.TryParse(queryByHome.areasId, out var areasId))
            {
                items = items.Where(x => x.AreasId == areasId);
            }

            if (!string.IsNullOrEmpty(queryByHome.cityId) && Guid.TryParse(queryByHome.cityId, out var cityId))
            {
                items = items.Where(x => x.CityId == cityId);
            }
            if (!string.IsNullOrEmpty(queryByHome.favoritesId) && Guid.TryParse(queryByHome.favoritesId, out var favoritesId))
            {
                items = items.Where(x => x.FavoritesId == favoritesId);
            }
            if (!string.IsNullOrEmpty(queryByHome.roomstyleId) && Guid.TryParse(queryByHome.roomstyleId, out var roomstyleId))
            {
                items = items.Where(x => x.RoomstyleId == roomstyleId);
            }
            var paginatedResult = PaginatedList<HousesQuery>.ToPageList(items.ToList(), queryByHome.page, queryByHome.limit);
            return new PagedDataResponse<HousesQuery>(paginatedResult, 200, items.Count());
        }

        public DataResponse<HousesQuery> ItemById(Guid id)
        {
            var query = _housesRepository.GetAllData().AsQueryable();
            var areas = _areasRepository.GetAllData();
            var roomstyles = _roomstyleRepository.GetAllData();
            var cities = _cityRepository.GetAllData();
            var users = _usersRepository.GetAllData();

            var item = from houses in query
                       join area in areas on houses.AreasId equals area.AreasId
                       join roomstyle in roomstyles on houses.RoomstyleId equals roomstyle.RoomstyleId
                       join user in users on houses.UserId equals user.UserId
                       join city in cities on houses.CityId equals city.CityId
                       where houses.HousesId == id
                       select new HousesQuery
                       {
                           HousesId = houses.HousesId,
                           HousesName = houses.HousesName,
                           Title = houses.Title,
                           Interior = houses.Interior,
                           AddressHouses = houses.AddressHouses,
                           DateSubmitted = houses.DateSubmitted,
                           AreasId = houses.AreasId,
                           AreasName = area.AreasName,
                           RoomstyleId = houses.RoomstyleId,
                           RoomstyleName = roomstyle.RoomstyleName,
                           UsersId = houses.UserId,
                           UsersName = user.FullName,
                           CityId = houses.CityId,
                           CityName = city.CityName,

                       };

            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }

            return new DataResponse<HousesQuery>(item.SingleOrDefault(), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<HousesQuery> Relatedhousess(CommonQueryByHome queryByHome, Guid id)
        {
            var query = _housesRepository.GetAllData().AsQueryable();
            var areas = _areasRepository.GetAllData();
            var roomstyles = _roomstyleRepository.GetAllData();
            var cities = _cityRepository.GetAllData();
            var users = _usersRepository.GetAllData();

            var items = from houses in query
                        join area in areas on houses.AreasId equals area.AreasId
                        join roomstyle in roomstyles on houses.RoomstyleId equals roomstyle.RoomstyleId
                        join user in users on houses.UserId equals user.UserId
                        join city in cities on houses.CityId equals city.CityId
                        where houses.HousesId == id
                        select new HousesQuery
                        {
                            HousesId = houses.HousesId,
                            HousesName = houses.HousesName,
                            Title = houses.Title,
                            Interior = houses.Interior,
                            AddressHouses = houses.AddressHouses,
                            DateSubmitted = houses.DateSubmitted,
                            AreasId = houses.AreasId,
                            AreasName = area.AreasName,
                            RoomstyleId = houses.RoomstyleId,
                            RoomstyleName = roomstyle.RoomstyleName,
                            UsersId = houses.UserId,
                            UsersName = user.FullName,
                            CityId = houses.CityId,
                            CityName = city.CityName,

                        };

            items = items.Where(x => x.HousesId != id);

            if (!string.IsNullOrEmpty(queryByHome.keyword))
            {
                items = items.Where(x =>
                                         x.HousesName.Contains(queryByHome.keyword) ||
                                         x.Title.Contains(queryByHome.keyword) ||
                                         x.Interior.Contains(queryByHome.keyword) ||
                                         x.AddressHouses.Contains(queryByHome.keyword) ||
                                         x.AreasName.Contains(queryByHome.keyword) ||
                                         x.CityName.Contains(queryByHome.keyword) ||
                                         x.UsersName.Contains(queryByHome.keyword) ||
                                         x.RoomstyleName.Contains(queryByHome.keyword));
            }

            if (!string.IsNullOrEmpty(queryByHome.favoritesId) && Guid.TryParse(queryByHome.favoritesId, out var favoritesId))
            {
                items = items.Where(x => x.FavoritesId.Equals(favoritesId));
            }
            if (!string.IsNullOrEmpty(queryByHome.roomstyleId) && Guid.TryParse(queryByHome.roomstyleId, out var roomstyleId))
            {
                items = items.Where(x => x.RoomstyleId == roomstyleId);
            }

            if (!string.IsNullOrEmpty(queryByHome.areasId) && Guid.TryParse(queryByHome.areasId, out var areasId))
            {
                items = items.Where(x => x.AreasId == areasId);
            }

            if (!string.IsNullOrEmpty(queryByHome.cityId) && Guid.TryParse(queryByHome.cityId, out var cityId))
            {
                items = items.Where(x => x.CityId == cityId);
            }


            var paginatedResult = PaginatedList<HousesQuery>.ToPageList(items.ToList(), queryByHome.page, queryByHome.limit);
            return new PagedDataResponse<HousesQuery>(paginatedResult, 200, items.Count());
        }

        public PagedDataResponse<HousesQuery> RelatedHouses(CommonQueryByHome queryByHome, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
