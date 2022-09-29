FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk
WORKDIR /app

COPY ./ ./
RUN dotnet publish -c Release -o out

RUN apt-get update
RUN apt-get install -y wget
RUN apt-get install -y curl

RUN cd /app
RUN apt-get update
RUN apt-get install -y --no-install-recommends
RUN apt-get install -y --no-install-recommends zlib1g
RUN apt-get install -y --no-install-recommends fontconfig
RUN apt-get install -y --no-install-recommends libfreetype6
RUN apt-get install -y --no-install-recommends libx11-6
RUN apt-get install -y --no-install-recommends libxext6
RUN apt-get install -y --no-install-recommends libxrender1
RUN curl -o /usr/lib/libwkhtmltox.so --location https://github.com/rdvojmoc/DinkToPdf/raw/v1.0.8/v0.12.4/64%20bit/libwkhtmltox.so

RUN wget https://github.com/rdvojmoc/DinkToPdf/blob/master/v0.12.4/64%20bit/libwkhtmltox.dll
RUN wget https://github.com/rdvojmoc/DinkToPdf/blob/master/v0.12.4/64%20bit/libwkhtmltox.dylib
RUN wget https://github.com/rdvojmoc/DinkToPdf/blob/master/v0.12.4/64%20bit/libwkhtmltox.so

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=sdk /app/out ./
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet tsb.mininal.policy.engine.dll
# ENTRYPOINT ["dotnet", "tsb.mininal.policy.engine.dll"]
# DOCKER BUILD COMMAND: "docker build -t tsb_api ."