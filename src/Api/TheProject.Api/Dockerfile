#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Api/TheProject.Api/TheProject.Api.csproj", "Api/TheProject.Api/"]
COPY ["CrossCutting/TheProject.BackgroundManager/TheProject.BackgroundManager.csproj", "CrossCutting/TheProject.BackgroundManager/"]
COPY ["Api/TheProject.Application/TheProject.Application.csproj", "Api/TheProject.Application/"]
COPY ["CrossCutting/TheProject.Bus/TheProject.Bus.csproj", "CrossCutting/TheProject.Bus/"]
COPY ["Api/TheProject.Core/TheProject.Core.csproj", "Api/TheProject.Core/"]
COPY ["Shared/TheProject.Shared/TheProject.Shared.csproj", "Shared/TheProject.Shared/"]
COPY ["Api/TheProject.Domain/TheProject.Domain.csproj", "Api/TheProject.Domain/"]
COPY ["Api/TheProject.Infra/TheProject.Infra.csproj", "Api/TheProject.Infra/"]
RUN dotnet restore "Api/TheProject.Api/TheProject.Api.csproj"
COPY . .
WORKDIR "/src/Api/TheProject.Api"
RUN dotnet build "TheProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheProject.Api.dll"]