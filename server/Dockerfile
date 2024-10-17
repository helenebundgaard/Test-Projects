# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy the main project file and restore dependencies
COPY server.csproj ./
RUN dotnet restore server.csproj

# Copy everything else and build the project
COPY . ./
RUN dotnet publish server.csproj -c Release -o out

# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
EXPOSE 12000
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "server.dll"]
