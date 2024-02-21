FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["source/BaseQueryService/QueryWebService/QueryWebService.csproj", "source/BaseQueryService/QueryWebService/"]
RUN dotnet restore "source/BaseQueryService/QueryWebService/QueryWebService.csproj"
COPY . .
WORKDIR "/src/source/BaseQueryService/QueryWebService"
RUN dotnet build "QueryWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueryWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueryWebService.dll"]
