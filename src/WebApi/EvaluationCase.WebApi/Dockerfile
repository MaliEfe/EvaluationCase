#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApi/EvaluationCase.WebApi/EvaluationCase.WebApi.csproj", "src/WebApi/EvaluationCase.WebApi/"]
COPY ["src/Core/EvaluationCase.Application/EvaluationCase.Application.csproj", "src/Core/EvaluationCase.Application/"]
COPY ["src/Core/EvaluationCase.Domain/EvaluationCase.Domain.csproj", "src/Core/EvaluationCase.Domain/"]
COPY ["src/Infrastructure/EvaluationCase.Persistence/EvaluationCase.Persistence.csproj", "src/Infrastructure/EvaluationCase.Persistence/"]
RUN dotnet restore "src/WebApi/EvaluationCase.WebApi/EvaluationCase.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi/EvaluationCase.WebApi"
RUN dotnet build "EvaluationCase.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EvaluationCase.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvaluationCase.WebApi.dll"]