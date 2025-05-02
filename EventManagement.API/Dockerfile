# Base image for ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image using SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy entire solution
COPY .. .

# Restore dependencies
WORKDIR /src/EventManagement.API
RUN dotnet restore

# Publish the app
RUN dotnet publish -c Release -o /app/publish

# Final stage: use base and copy the published app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EventManagement.API.dll"]
