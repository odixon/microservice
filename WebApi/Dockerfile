#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

ARG CONSUL_HOST
ARG CONSUL_PORT
ARG DB_CONNECTION_STRING

WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
RUN dotnet restore "WebApi/WebApi.csproj"
WORKDIR /src/WebApi
COPY WebApi/ .

RUN sed -i "s/CONSUL_HOST/${CONSUL_HOST}/g" appsettings.json \
   && sed -i "s/CONSUL_PORT/${CONSUL_PORT}/g" appsettings.json \
   && sed -i "s/DB_CONNECTION_STRING/${DB_CONNECTION_STRING}/g" appsettings.json

RUN cat appsettings.json
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
