FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["com.btc.app.system/com.btc.app.system.csproj","com.btc.app.system/"]
COPY ["com.btc.dataaccess/com.btc.process.dataaccess.csproj","com.btc.dataaccess/"]
COPY ["com.btc.manager/com.btc.process.manager.csproj","com.btc.manager/"]
COPY ["com.btc.process.utility/com.btc.process.utility.csproj","com.btc.process/"]

COPY . .
WORKDIR "/src/com.btc.app.system"
RUN dotnet build "com.btc.app.system.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "com.btc.app.system.csproj" -c $BUILD_CONFIGURATION -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","com.btc.app.system.dll"]