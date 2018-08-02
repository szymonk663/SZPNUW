FROM microsoft/aspnetcore:2.0 AS base
RUN apt-get update 
RUN apt-get install gnupg wget git unzip -y
RUN curl -sL https://deb.nodesource.com/setup_8.x | bash -
RUN apt-get install nodejs -y
ENV ASPNETCORE_URLS=http://+:7001
WORKDIR /app
EXPOSE 7001

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SZPNUW.Portal.sln ./
COPY SZPNUW.Portal/SZPNUW.Portal.csproj SZPNUW.Portal/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/SZPNUW.Portal
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SZPNUW.Portal.dll"]
