FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Patient.API/Patient.API.csproj", "Patient.API/"]
COPY ["Patient.Application/Patient.Application.csproj", "Patient.Application/"]
COPY ["Patient.Domain/Patient.Domain.csproj", "Patient.Domain/"]
COPY ["Patient.Persistence/Patient.Persistence.csproj", "Patient.Persistence/"]
COPY ["Patient.Settings/Patient.Settings.csproj", "Patient.Settings/"]
RUN dotnet restore "./Patient.API/./Patient.API.csproj"
COPY . .
WORKDIR "/src/Patient.API"
RUN dotnet build "./Patient.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./Patient.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Patient.API.dll"]