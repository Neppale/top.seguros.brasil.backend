FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk
WORKDIR /app

RUN apt update
RUN apt-get update
RUN apt-get install -y wget
RUN apt-get install -y curl
RUN apt install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /lib/x86_64-linux-gnu/libgdiplus.so
RUN apt-get install -y --no-install-recommends zlib1g fontconfig libfreetype6 libx11-6 libxext6 libxrender1 wget gdebi
RUN wget https://github.com/wkhtmltopdf/wkhtmltopdf/releases/download/0.12.5/wkhtmltox_0.12.5-1.stretch_amd64.deb
RUN gdebi --n wkhtmltox_0.12.5-1.stretch_amd64.deb
RUN ln -s /usr/local/lib/libwkhtmltox.so /usr/lib/libwkhtmltox.so

COPY ./ ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=sdk /app/out ./
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet tsb.mininal.policy.engine.dll
# ENTRYPOINT ["dotnet", "tsb.mininal.policy.engine.dll"]
# DOCKER BUILD COMMAND: "docker build -t tsb_api ."