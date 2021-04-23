FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EveryGraph/EveryGraph.csproj", "EveryGraph/"]
RUN dotnet restore "EveryGraph/EveryGraph.csproj"
COPY . .
WORKDIR "/src/EveryGraph"
RUN dotnet build "EveryGraph.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EveryGraph.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EveryGraph.dll"]