FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src/Merchandise-service
COPY . .
RUN dotnet restore "Merchandise-service.csproj"
RUN dotnet build "Merchandise-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Merchandise-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Merchandise-service.dll"]
