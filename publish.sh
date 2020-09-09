#!/usr/bin/env bash

rm -rf publish Server/wwwroot
yarn install
yarn build
dotnet publish WiFiSurveyor -c Release -o publish