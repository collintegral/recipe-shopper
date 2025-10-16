# Use a minimal base image
FROM debian:bullseye-slim

# Set environment variables
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true \
    ASPNETCORE_URLS=http://+:80 \
    ASPNETCORE_ENVIRONMENT=Production

# Create app directory
WORKDIR /app

# Copy published files
COPY ./publish .

# Expose port 80
EXPOSE 80

# Run the app
ENTRYPOINT ["./RecipeShopper"]