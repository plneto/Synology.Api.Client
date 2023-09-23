FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["../samples/Dotnet7Sample/Dotnet7Sample.csproj", "../samples/Dotnet7Sample/"]
RUN dotnet restore "../samples/Dotnet7Sample/Dotnet7Sample.csproj"
COPY . .
WORKDIR "/src/../samples/Dotnet7Sample"
RUN dotnet build "Dotnet7Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet7Sample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet7Sample.dll"]
