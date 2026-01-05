# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080

COPY --from=build /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CosmeticsStore.dll"]
