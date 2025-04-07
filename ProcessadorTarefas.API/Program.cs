using FluentValidation.AspNetCore;
using ProcessadorTarefas.API;
using ProcessadorTarefas.API.Extensions;
using ProcessadorTarefas.Application.Commands.CriarTarefa;
using ProcessadorTarefas.Application.Queries.BuscarTarefa;
using ProcessadorTarefas.Application.Validators;
using ProcessadorTarefas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CriarTarefaCommandValidator>());

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CriarTarefaCommand).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
