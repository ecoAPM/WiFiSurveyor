# Wi-Fi Surveyor

Visualize Wi-Fi signal strength over a geographic area

## Quick Start

1. Download and extract the [latest release](/releases/latest) for your operating system

1. From a command line, run:
   - `sudo WiFiSurveyor` on Linux (must be `root`)
   - `WiFiSurveyor.exe` on Windows

1. Browse to `http://localhost:5000/`
 
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

- Windows uses the first Wi-Fi adapter

- No macOS support (#2)

- Resizing the browser window will not scale readings with the background image: once you start taking readings, don't resize your window (rotating your device and rotating back should be OK)

## Contributing

### Requirements

- .NET Core SDK 3.1
- Node.JS with `yarn`

### Building from source

- Run `dotnet run` (with `sudo` for Linux) from the `Server` directory to start the beck-end server
- Run `yarn dev` from the repo root directory to start the front-end development server
- Browse to `http://localhost:1234`
- Back-end and front-end can be stopped and restarted independently during inner dev loop

### Running tests

- Run `dotnet test` from the repo root directory for back-end tests
- Run `yarn test` from the repo root directory for front-end tests

### Publishing

- Run `./publish.sh` from the repo root directory to publish a production-ready version to the `publish` directory