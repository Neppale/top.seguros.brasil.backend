FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk
WORKDIR /app

COPY ./ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=sdk /app/out ./
CMD ASPNETCORE_URLS="http://*:$PORT dotnet WebApplication.dll" dotnet tsb.mininal.policy.engine.dll
# ENTRYPOINT ["dotnet", "tsb.mininal.policy.engine.dll"]
# DOCKER BUILD COMMAND: "docker build -t tsb_api ."