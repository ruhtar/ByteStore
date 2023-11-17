FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ByteStore.Tests/ByteStore.Tests.csproj", "ByteStore.Tests/"]
RUN dotnet restore "ByteStore.Tests/ByteStore.Tests.csproj"
COPY . .
WORKDIR "/src/ByteStore.Tests"
RUN dotnet build "ByteStore.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ByteStore.Tests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ByteStore.Tests.dll"]
