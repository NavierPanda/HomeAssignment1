FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["HomeAssignment/HomeAssignment.WebApi.csproj", "HomeAssignment/"]
RUN dotnet restore "HomeAssignment/HomeAssignment.WebApi.csproj"
COPY . .
WORKDIR "/src/HomeAssignment"
RUN dotnet build "HomeAssignment.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeAssignment.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssignment.WebApi.dll"]
