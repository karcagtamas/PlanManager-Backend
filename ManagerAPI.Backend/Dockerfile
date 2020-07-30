FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ManagerAPI.Backend/ManagerAPI.Backend.csproj", "ManagerAPI.Backend/"]
COPY ["MovieCorner.Services/MovieCorner.Services.csproj", "MovieCorner.Services/"]
COPY ["ManagerAPI.Services/ManagerAPI.Services.csproj", "ManagerAPI.Services/"]
COPY ["ManagerAPI.Shared/ManagerAPI.Shared.csproj", "ManagerAPI.Shared/"]
COPY ["ManagerAPI.DataAccess/ManagerAPI.DataAccess.csproj", "ManagerAPI.DataAccess/"]
COPY ["ManagerAPI.Domain/ManagerAPI.Domain.csproj", "ManagerAPI.Domain/"]
COPY ["PlanManager.Services/PlanManager.Services.csproj", "PlanManager.Services/"]
COPY ["EventManager.Services/EventManager.Services.csproj", "EventManager.Services/"]
RUN dotnet restore "ManagerAPI.Backend/ManagerAPI.Backend.csproj"
COPY . .
WORKDIR "/src/ManagerAPI.Backend"
RUN dotnet build "ManagerAPI.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ManagerAPI.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManagerAPI.Backend.dll"]