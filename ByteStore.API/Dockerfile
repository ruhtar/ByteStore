#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ByteStore.API/ByteStore.API.csproj", "ByteStore.API/"]
COPY ["ByteStore.Application/ByteStore.Application.csproj", "ByteStore.Application/"]
COPY ["ByteStore.Domain/ByteStore.Domain.csproj", "ByteStore.Domain/"]
COPY ["ByteStore.Infrastructure/ByteStore.Infrastructure.csproj", "ByteStore.Infrastructure/"]
COPY ["ByteStore.Shared/ByteStore.Shared.csproj", "ByteStore.Shared/"]
RUN dotnet restore "ByteStore.API/ByteStore.API.csproj"
COPY . .
WORKDIR "/src/ByteStore.API"
RUN dotnet build "ByteStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ByteStore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ByteStore.API.dll"]