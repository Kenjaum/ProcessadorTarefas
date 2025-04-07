FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProcessadorTarefas.API/ProcessadorTarefas.API.csproj", "ProcessadorTarefas.API/"]
COPY ["ProcessadorTarefas.Application/ProcessadorTarefas.Application.csproj", "ProcessadorTarefas.Application/"]
COPY ["ProcessadorTarefas.Core/ProcessadorTarefas.Core.csproj", "ProcessadorTarefas.Core/"]
COPY ["ProcessadorTarefas.Infrastructure/ProcessadorTarefas.Infrastructure.csproj", "ProcessadorTarefas.Infrastructure/"]
RUN dotnet restore "ProcessadorTarefas.API/ProcessadorTarefas.API.csproj"

COPY . .
WORKDIR "/src/ProcessadorTarefas.API"
RUN dotnet build "ProcessadorTarefas.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessadorTarefas.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessadorTarefas.API.dll"]
