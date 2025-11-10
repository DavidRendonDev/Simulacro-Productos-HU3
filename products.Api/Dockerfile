FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["products.Api/products.Api.csproj", "products.Api/"]
COPY ["products.Application/products.Application.csproj", "products.Application/"]
COPY ["products.Domain/products.Domain.csproj", "products.Domain/"]
COPY ["products.Infrastructure/products.Infrastructure.csproj", "products.Infrastructure/"]
RUN dotnet restore "products.Api/products.Api.csproj"
COPY . .
WORKDIR "/src/products.Api"
RUN dotnet build "./products.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./products.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "products.Api.dll"]
