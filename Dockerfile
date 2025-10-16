# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "RecipeShopper.csproj" -c Release -r linux-x64 --self-contained true -o /app/publish

# Runtime stage
FROM debian:bullseye-slim
WORKDIR /app

# Install ICU
RUN apt-get update && apt-get install -y libicu-dev && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["./RecipeShopper"]