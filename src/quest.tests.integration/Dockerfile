FROM microsoft/aspnetcore-build:2.0.3 AS builder
ARG api
ARG integration

# Restore packages
COPY ${api}/*.csproj ${api}/
COPY ${integration}/*.csproj ${integration}/

WORKDIR /${integration}
RUN dotnet restore

COPY ${api} /${api}
COPY ${integration} /${integration}

RUN dotnet build

CMD ["bash"]