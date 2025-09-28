FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["eCommerce.API/eCommerce.API.csproj", "eCommerce.API/"]
RUN dotnet restore "eCommerce.API/eCommerce.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.API"
RUN dotnet build "./eCommerce.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./eCommerce.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV POSTGRES_USER=postgres
ENV POSTGRES_HOST=localhost
ENV POSTGRES_PORT=5432
ENV POSTGRES_PASSWORD=1234
ENV POSTGRES_DB=eCommerceUsers

ENTRYPOINT ["dotnet", "eCommerce.API.dll"]
