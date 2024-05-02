using AutoMapper;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Service;
using DormSearchBe.Domain.Mapping;
using DormSearchBe.Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace DormSearchBe.Application.Module
{
    public static class ApplicationModules
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddInfrastructureModule();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAreasService, AreasService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<IHousesService, HousesService>();
            services.AddScoped<IMessagesService, MessagesService>();
            services.AddScoped<IPricesService, PricesService>();
            services.AddScoped<IRatingsService, RatingsService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoomstyleService, RoomstyleService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
