FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["source/IdeaService/IdeaWebService/IdeaWebService.csproj", "source/IdeaService/IdeaWebService/"]
RUN dotnet restore "source/IdeaService/IdeaWebService/IdeaWebService.csproj"
COPY . .
WORKDIR "/src/source/IdeaService/IdeaWebService"
RUN dotnet build "IdeaWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdeaWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdeaWebService.dll"]
