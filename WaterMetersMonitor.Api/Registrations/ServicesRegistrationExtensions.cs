using AutoMapper;
using WaterMetersMonitor.Api.Models.Group;
using WaterMetersMonitor.Api.Models.User;
using WaterMetersMonitor.Api.Models.WaterMeter;
using WaterMetersMonitor.Application.Repositories;
using WaterMetersMonitor.Application.Services;
using WaterMetersMonitor.Domain.Entities;

namespace WaterMetersMonitor.Api.Registrations
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Group>, EntityRepository<Group>>();
            services.AddTransient<IRepository<User>, EntityRepository<User>>();
            services.AddTransient<IRepository<MainWaterMeter>, EntityRepository<MainWaterMeter>>();
            services.AddTransient<IRepository<MainWaterMeterValue>, EntityRepository<MainWaterMeterValue>>();
            services.AddTransient<IRepository<WaterMeter>, EntityRepository<WaterMeter>>();
            services.AddTransient<IRepository<WaterMeterValue>, EntityRepository<WaterMeterValue>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<ICalculationService, CalculationService>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<MainWaterMeterCreateDto, MainWaterMeter>();
                mc.CreateMap<GroupCreateDto, Group>();
                mc.CreateMap<UserCreateDto, User>();
                mc.CreateMap<WaterMeterCreateDto, WaterMeter>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
