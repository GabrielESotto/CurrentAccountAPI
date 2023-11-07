FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CurrentAccount.Api/CurrentAccount.Api.csproj", "CurrentAccount.Api/"]
COPY ["CurrentAccount.Data/CurrentAccount.Data.csproj", "CurrentAccount.Data/"]
COPY ["CurrentAccount.Domain/CurrentAccount.Domain.csproj", "CurrentAccount.Domain/"]
COPY ["CurrentAccount.Dto/CurrentAccount.Dto.csproj", "CurrentAccount.Dto/"]
RUN dotnet restore "CurrentAccount.Api/CurrentAccount.Api.csproj"
COPY . .
WORKDIR "/src/CurrentAccount.Api"
RUN dotnet build "CurrentAccount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrentAccount.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrentAccount.Api.dll"]