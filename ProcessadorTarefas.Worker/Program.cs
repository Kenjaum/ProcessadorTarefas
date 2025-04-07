using ProcessadorTarefas.Core.Services;
using ProcessadorTarefas.Infrastructure.MessageBus;
using ProcessadorTarefas.Worker;
using ProcessadorTarefas.Worker.Services.Implementations;
using ProcessadorTarefas.Worker.Services.Interfaces;
using ProcessadorTarefas.Worker.Strategies;
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ITarefaRepository, TarefaRepository>();
builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
builder.Services.AddSingleton<IProcessadorTarefaStrategy, EnviarEmailStrategy>();
builder.Services.AddSingleton<IProcessadorTarefaStrategy, GerarRelatorioStrategy>();
builder.Services.AddSingleton<ProcessadorTarefaService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
