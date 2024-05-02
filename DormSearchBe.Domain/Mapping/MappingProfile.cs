using AutoMapper;
using DormSearchBe.Domain.Dto.Areas;
using DormSearchBe.Domain.Dto.Auth;
using DormSearchBe.Domain.Dto.City;
using DormSearchBe.Domain.Dto.Favorites;
using DormSearchBe.Domain.Dto.Houses;
using DormSearchBe.Domain.Dto.Messages;
using DormSearchBe.Domain.Dto.Permission;
using DormSearchBe.Domain.Dto.Prices;
using DormSearchBe.Domain.Dto.Ratings;
using DormSearchBe.Domain.Dto.Role;
using DormSearchBe.Domain.Dto.Roomstyle;
using DormSearchBe.Domain.Dto.User;
using DormSearchBe.Domain.Entity;

namespace DormSearchBe.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<Permission, PermissionQuery>().ReverseMap();

            CreateMap<Areas, AreasDto>().ReverseMap();
            CreateMap<Areas, AreasQuery>().ReverseMap();

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityQuery>().ReverseMap();

            CreateMap<Favorites,FavoritesDto>().ReverseMap();

            CreateMap<Houses, HousesDto>().ReverseMap();
            CreateMap<Houses, HousesQuery>().ReverseMap();

            CreateMap<Message, MessageDto>().ReverseMap();

            CreateMap<Prices, PricesDto>().ReverseMap();
            CreateMap<Prices, PricesQuery>().ReverseMap();

            CreateMap<Ratings, RatingsChangeFeedback>().ReverseMap();
            CreateMap<Ratings, RatingsChangeStatus>().ReverseMap();
            CreateMap<Ratings, RatingsDto>().ReverseMap();
            CreateMap<Ratings, RatingsQuery>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleQuery>().ReverseMap();

            CreateMap<Roomstyle,RoomstyleDto>().ReverseMap();
            CreateMap<Roomstyle, RoomstyleQuery>().ReverseMap();

            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<Refresh_Token, RefreshTokenDto>().ReverseMap();
            CreateMap<User, UserQuery>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            
        }
    }
}
