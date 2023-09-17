#Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./AP-ShopBE/AP-ShopBE.csproj" --disable-parallel
RUN dotnet publish "./AP-ShopBE/AP-ShopBE.csproj" -c release -o /app --no-restore

#Serve stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "AP-ShopBE.dll"]
