
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

COPY ./publish .
ENTRYPOINT ["dotnet", "FinalProjectASP_Net.dll"]