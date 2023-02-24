#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/Company.Crm.Web.Api/Company.Crm.Web.Api.csproj", "src/Web/Company.Crm.Web.Api/"]
RUN dotnet restore "src/Web/Company.Crm.Web.Api/Company.Crm.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Web/Company.Crm.Web.Api"
RUN dotnet build "Company.Crm.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Crm.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Crm.Web.Api.dll"]