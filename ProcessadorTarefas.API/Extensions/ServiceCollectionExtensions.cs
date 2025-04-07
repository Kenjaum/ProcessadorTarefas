using ProcessadorTarefas.Core.Repositories;
using ProcessadorTarefas.Core.Services;
using ProcessadorTarefas.Infrastructure.MessageBus;
using ProcessadorTarefas.Infrastructure.Persistence;
using ProcessadorTarefas.Infrastructure.Persistence.Repositories;

namespace ProcessadorTarefas.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITarefaRepository, TarefaRepositoy>();
            services.AddScoped<IMessageBusService, MessageBusService>();
            services.AddScoped<IMongoDbContext, MongoDbContext>();

            return services;
        }
    }
}
