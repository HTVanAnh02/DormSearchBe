using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Common.Utilities;
using DormSearchBe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;


namespace DormSearchBe.Infrastructure.Module
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, ApprovalAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, ApprovalAuthorizationPolicyProvider>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IAreasRepository, AreasRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddScoped<IHousesRepository, HousesRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IPricesRepository,PricesRepository>();
            services.AddScoped<IRatingsRepository,RatingsRepository>();
            services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoomstyleRepository,RoomstyleRepository>();
           
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
