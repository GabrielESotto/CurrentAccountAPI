using CurrentAccount.Data.Context;
using CurrentAccount.Data.Repositories;
using CurrentAccount.Domain.AccountsStatements.Commands.Command;
using CurrentAccount.Domain.AccountsStatements.Interfaces;
using CurrentAccount.Domain.Notifications;
using CurrentAccount.Domain.Notifications.Interfaces;
using Nuuvify.CommonPack.Middleware;
using Nuuvify.CommonPack.Middleware.Abstraction;
using System.Reflection;

namespace CurrentAccount.Api.Configuration
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("CurrentAccount.Dto"));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<SqlDbContext>();

            services.AddScoped<IAccountStatementRepository, AccountStatementRepository>();

            services.AddScoped<INotification, Notificator>();
            services.AddScoped<IAccountStatement, AccountStatementCommandHandler>();

            services.AddScoped<IConfigurationCustom, ConfigurationCustom>();

            return services;
        }
    }
}
