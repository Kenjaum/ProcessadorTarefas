using ProcessadorTarefas.Worker.EnviarEmail;
using ProcessadorTarefas.Worker.EnviarEmail.Services.Implementations;
using ProcessadorTarefas.Worker.EnviarEmail.Services.Interfaces;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ITarefaRepository, TarefaRepository>();
builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
