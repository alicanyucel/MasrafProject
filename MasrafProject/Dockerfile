FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MasrafProject.WebAPI/MasrafProject.WebAPI.csproj", "MasrafProject.WebAPI/"]
COPY ["MasrafProject.Application/MasrafProject.Application.csproj", "MasrafProject.Application/"]
COPY ["MasrafProject.Domain/MasrafProject.Domain.csproj", "MasrafProject.Domain/"]
COPY ["MasrafProject.Infrastructure/MasrafProject.Infrastructure.csproj", "MasrafProject.Infrastructure/"]
RUN dotnet restore "MasrafProject.WebAPI/MasrafProject.WebAPI.csproj"
COPY . .
WORKDIR "/src/MasrafProject.WebAPI"
RUN dotnet build "MasrafProject.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasrafProject.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasrafProject.WebAPI.dll"]
