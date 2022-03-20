FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY HomeAssignment.WebApi/HomeAssignment.WebApi.csproj ./HomeAssignment.WebApi/
RUN dotnet restore ./HomeAssignment.WebApi/HomeAssignment.WebApi.csproj
COPY . .
WORKDIR /src/HomeAssignment.WebApi
RUN dotnet build HomeAssignment.WebApi.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish HomeAssignment.WebApi.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssignment.WebApi.dll"]
