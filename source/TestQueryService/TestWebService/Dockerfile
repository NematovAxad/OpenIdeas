FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["source/TestQueryService/TestWebService/TestWebService.csproj", "source/TestQueryService/TestWebService/"]
RUN dotnet restore "source/TestQueryService/TestWebService/TestWebService.csproj"
COPY . .
WORKDIR "/src/source/TestQueryService/TestWebService"
RUN dotnet build "TestWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestWebService.dll"]
