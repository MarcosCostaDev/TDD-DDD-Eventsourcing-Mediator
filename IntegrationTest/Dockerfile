#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IntegrationTest/IntegrationTest.csproj", "IntegrationTest/"]
COPY ["IntegrationTest.Core/IntegrationTest.Core.csproj", "IntegrationTest.Core/"]
COPY ["IntegrationTest.Domain/IntegrationTest.Domain.csproj", "IntegrationTest.Domain/"]
COPY ["IntegrationTest.Bus/IntegrationTest.Bus.csproj", "IntegrationTest.Bus/"]
COPY ["IntegrationTest.BackgroundManager/IntegrationTest.BackgroundManager.csproj", "IntegrationTest.BackgroundManager/"]
COPY ["IntegrationTest.Infra/IntegrationTest.Infra.csproj", "IntegrationTest.Infra/"]
RUN dotnet restore "IntegrationTest/IntegrationTest.csproj"
COPY . .
WORKDIR "/src/IntegrationTest"
RUN dotnet build "IntegrationTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntegrationTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntegrationTest.dll"]
