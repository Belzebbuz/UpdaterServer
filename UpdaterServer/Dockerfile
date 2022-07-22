#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 9092
EXPOSE 9093

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UpdaterServer/UpdaterServer.csproj", "UpdaterServer/"]
RUN dotnet restore "UpdaterServer/UpdaterServer.csproj"
COPY . .
WORKDIR "/src/UpdaterServer"
RUN dotnet build "UpdaterServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UpdaterServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpdaterServer.dll"]