FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["TradeArt/TradeArt.WebApi.csproj", "TradeArt/"]
RUN dotnet restore "TradeArt/TradeArt.WebApi.csproj"
COPY . .
WORKDIR "/src/TradeArt"
RUN dotnet build "TradeArt.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TradeArt.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradeArt.WebApi.dll"]
