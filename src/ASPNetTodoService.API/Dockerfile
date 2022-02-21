#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ASPNetTodoService.API/ASPNetTodoService.API.csproj", "src/ASPNetTodoService.API/"]
COPY ["src/ASPNetTodoService.Domain/ASPNetTodoService.Domain.csproj", "src/ASPNetTodoService.Domain/"]
COPY ["src/ASPNetTodoService.Infrastructure/ASPNetTodoService.Infrastructure.csproj", "src/ASPNetTodoService.Infrastructure/"]
RUN dotnet restore "src/ASPNetTodoService.API/ASPNetTodoService.API.csproj"
COPY . .
WORKDIR "/src/src/ASPNetTodoService.API"
RUN dotnet build "ASPNetTodoService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASPNetTodoService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPNetTodoService.API.dll"]