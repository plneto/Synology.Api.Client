FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["../samples/Dotnet8Sample/Dotnet8Sample.csproj", "../samples/Dotnet8Sample/"]
RUN dotnet restore "../samples/Dotnet8Sample/Dotnet8Sample.csproj"
COPY . .
WORKDIR "/src/../samples/Dotnet8Sample"
RUN dotnet build "Dotnet8Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet8Sample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet8Sample.dll"]
