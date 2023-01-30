FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT = "Development"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CardGame.API/CardGame.API.csproj", "CardGame.API/"]
RUN dotnet restore "CardGame.API/CardGame.API.csproj"
COPY . .
WORKDIR "/src/CardGame.API"
RUN dotnet build "CardGame.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CardGame.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardGame.API.dll"]
