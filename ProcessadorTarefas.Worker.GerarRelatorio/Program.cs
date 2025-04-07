using ProcessadorTarefas.Worker.GerarRelatorio;
using ProcessadorTarefas.Worker.GerarRelatorio.Services.Implementations;
using ProcessadorTarefas.Worker.GerarRelatorio.Services.Interfaces;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ITarefaRepository, TarefaRepository>();
builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
