#!/bin/bash
set -e
docker-compose stop
docker-compose rm -f

docker-compose \
    -f docker-compose.yml \
    up --build -d

docker-compose \
    -f docker-compose.integration.yml \
    build

docker-compose \
    -f docker-compose.integration.yml \
    run --rm quests.integration.tests \
    dotnet test --no-build