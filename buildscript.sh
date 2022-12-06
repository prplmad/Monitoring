#!/bin/bash

dotnet restore "WebApp/WebApp.csproj"
dotnet build "WebApp/WebApp.csproj" -c Release -o ../App/build
dotnet publish "WebApp/WebApp.csproj" -c Release -o ../App/publish

cd WebApp/client
cp ./package.json ../../../App/publish/client/package.json
cd ../../../App/publish/client
npm install
npm run build



