FROM microsoft/aspnetcore:2.0 AS base
ENV ASPNETCORE_URLS=http://+:7002
WORKDIR /app
EXPOSE 7002

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SZPNUW.Portal.sln ./
COPY Microservices/SZPNUW.WebAPI.Account/SZPNUW.WebAPI.Account.csproj Microservices/SZPNUW.WebAPI.Account/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Microservices/SZPNUW.WebAPI.Account
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SZPNUW.WebAPI.Account.dll"]
