#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["csharp/GameOfLife.CSharp.Api/GameOfLife.CSharp.Api.csproj", "csharp/GameOfLife.CSharp.Api/"]
COPY ["csharp/GameOfLife.CSharp.Engine/GameOfLife.CSharp.Engine.csproj", "csharp/GameOfLife.CSharp.Engine/"]
RUN dotnet restore "csharp/GameOfLife.CSharp.Api/GameOfLife.CSharp.Api.csproj"
COPY . .
WORKDIR "/src/csharp/GameOfLife.CSharp.Api"
RUN dotnet build "GameOfLife.CSharp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameOfLife.CSharp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameOfLife.CSharp.Api.dll"]