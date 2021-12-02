# Wi-Fi Surveyor

[![Latest Release](https://img.shields.io/github/v/release/ecoAPM/WiFiSurveyor?label=Install&logo=github&include_prereleases)](https://github.com/ecoAPM/WiFiSurveyor/releases)
[![Build Status](https://github.com/ecoAPM/WiFiSurveyor/workflows/CI/badge.svg)](https://github.com/ecoAPM/WiFiSurveyor/actions)

App
[![App Coverage](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-App&metric=coverage)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-App)
[![App Maintainability](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-App&metric=sqale_rating)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-App)
[![App Reliability](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-App&metric=reliability_rating)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-App)
[![App Security](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-App&metric=security_rating)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-App)

Server
[![Server Coverage](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-Server&metric=coverage)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-Server)
[![Server Maintainability](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-Server&metric=sqale_rating)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-Server)
[![Server Reliability](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-Server&metric=reliability_rating)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-Server)
[![Server Security](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_WiFiSurveyor-Server&metric=security_rating)](https://sonarcloud.io/summary/overall?id=ecoAPM_WiFiSurveyor-Server)

Visualize Wi-Fi signal strength over a geographic area

## Quick Start

1. Download and extract the [latest release](https://github.com/ecoAPM/WiFiSurveyor/releases) for your operating system

1. Extract the archive to your directory of choice

1. Launch the executable for your OS:
   - `WiFiSurveyor.exe` on Windows
   - `WiFiSurveyor` on MacOS
   - `sudo ./WiFiSurveyor` on Linux (must be `root`)

1. Wait for the app to appear in your browser
 
1. Under "Background", select a floorplan or map image representing the area to survey 

1. Select your SSID from the "Access Point" dropdown menu

1. Traverse the area to survey, clicking on corresponding map points that represent your location

1. Once data has been collected, select other access points or change filters to display updated coverage

### Access Point Filters

- The default selection of both "Group by SSID" and "Combine 2.4 + 5GHz" will show one option per SSID

- Selecting only "Group by SSID" will show one option for each frequency that an SSID receives

- Unselecting both "Group by SSID" (which disables "Combine 2.4 + 5GHz") will show every device for every SSID available, on both frequencies

### Selecting a background

- Supports all file types used for CSS `background-image`

- The "Pixelate" option is good for floor plans with low resolutions (less than 1px/in²) so straight lines maintain hard edges

## Limitations

Contibutions are welcome for improving the following:

- Linux uses the device named `wlan0`

- Windows uses the "first" Wi-Fi adapter

- Resizing the browser window will not scale readings with the background image: once you start taking readings, don't resize your window (rotating your device and rotating back should be OK)

## Contributing

### Requirements

- .NET Core SDK 3.1
- Node.JS with `yarn`

### Building from source

- Run `dotnet run` (with `sudo` for Linux) from the `Server` directory to start the back-end server
- Run `yarn dev` from the repo root directory to start the front-end development server
- Browse to `http://localhost:1234`
- Back-end and front-end can be stopped and restarted independently during inner dev loop

### Running tests

- Run `dotnet test` from the repo root directory for back-end tests
- Run `yarn test` from the repo root directory for front-end tests

### Publishing

- Run `./publish.sh` from the repo root directory to publish a production-ready version to the `publish` directory
