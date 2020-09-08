#!/usr/bin/env bash

rm -rf publish WiFiHeatMap/wwwroot
yarn install
yarn build
dotnet publish WiFiHeatMap -c Release -o publish