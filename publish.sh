#!/usr/bin/env bash
set -e

rm -rf publish Server/wwwroot
yarn install
yarn build
dotnet publish Server -c Release -o publish