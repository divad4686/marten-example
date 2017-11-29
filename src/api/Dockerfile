FROM microsoft/aspnetcore-build:2.0.3 AS builder
ARG api

# Restore packages
COPY ${api}/*.csproj ${api}/

RUN dotnet restore /${api}

## Copy all sources
COPY ${api} /${api}

##### Publish project #####
RUN dotnet publish /${api} --output /publish --configuration Release

# Stage 2
FROM microsoft/aspnetcore:2.0.3
WORKDIR /publish
COPY --from=builder /publish .
ENTRYPOINT ["dotnet", "api.dll"]
