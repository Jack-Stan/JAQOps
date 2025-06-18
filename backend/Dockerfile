FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["JAQOps.API/JAQOps.API.csproj", "JAQOps.API/"]
COPY ["JAQOps.Business/JAQOps.Business.csproj", "JAQOps.Business/"]
COPY ["JAQOps.Data/JAQOps.Data.csproj", "JAQOps.Data/"]
RUN dotnet restore "JAQOps.API/JAQOps.API.csproj"
COPY . .
WORKDIR "/src/JAQOps.API"
RUN dotnet build "JAQOps.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JAQOps.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JAQOps.API.dll"]
