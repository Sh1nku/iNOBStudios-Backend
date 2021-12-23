FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine as build
WORKDIR /app
COPY . .
RUN dotnet build --configuration Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "iNOBStudios.dll"]