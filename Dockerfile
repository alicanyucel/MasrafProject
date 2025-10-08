FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MasrafProject/MasrafProject.WebAPI/MasrafProject.WebAPI.csproj", "MasrafProject/MasrafProject.WebAPI/"]
COPY ["MasrafProject/MasrafProject.Application/MasrafProject.Application.csproj", "MasrafProject/MasrafProject.Application/"]
COPY ["MasrafProject/MasrafProject.Domain/MasrafProject.Domain.csproj", "MasrafProject/MasrafProject.Domain/"]
COPY ["MasrafProject/MasrafProject.Infrastructure/MasrafProject.Infrastructure.csproj", "MasrafProject/MasrafProject.Infrastructure/"]
RUN dotnet restore "MasrafProject/MasrafProject.WebAPI/MasrafProject.WebAPI.csproj"
COPY . .
WORKDIR "/src/MasrafProject/MasrafProject.WebAPI"
RUN dotnet build "MasrafProject.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasrafProject.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasrafProject.WebAPI.dll"]
