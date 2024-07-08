FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "SGS.WebApi/SGS.WebApi.csproj"

RUN dotnet build "SGS.WebApi/SGS.WebApi.csproj" -c Release -o /app/build

RUN dotnet publish "SGS.WebApi/SGS.WebApi.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SGS.WebApi.dll"]