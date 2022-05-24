#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ticket.WebApi/Ticket.WebApi.csproj", "Ticket.WebApi/"]
COPY ["Ticket.Application/Ticket.Application.csproj", "Ticket.Application/"]
COPY ["Ticket.Domain/Ticket.Domain.csproj", "Ticket.Domain/"]
COPY ["Ticket.Business/Ticket.Business.csproj", "Ticket.Business/"]
COPY ["Ticket.Data/Ticket.Data.csproj", "Ticket.Data/"]
RUN dotnet restore "Ticket.WebApi/Ticket.WebApi.csproj"
COPY . .
WORKDIR "/src/Ticket.WebApi"
RUN dotnet build "Ticket.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticket.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://*:4500"
ENTRYPOINT ["dotnet", "Ticket.WebApi.dll"]