FROM microsoft/dotnet:2.1-sdk-stretch-arm32v7 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY /src .

RUN dotnet publish -c Release -o out -r linux-arm

#FROM build-env

ENTRYPOINT ["dotnet", "RaspberryPiTest/out/RaspberryPiTest.dll"]