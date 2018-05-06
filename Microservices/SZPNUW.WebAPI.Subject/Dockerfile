FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SZPNUW.Portal.sln ./
COPY Microservices/SZPNUW.WebAPI.Subject/SZPNUW.WebAPI.Subject.csproj SZPNUW.WebAPI.Subject/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Microservices/SZPNUW.WebAPI.Subject
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SZPNUW.WebAPI.Subject.dll"]
