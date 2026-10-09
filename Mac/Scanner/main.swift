import AppKit
import CoreLocation
import CoreWLAN
import Foundation

final class Scanner: NSObject, NSApplicationDelegate, CLLocationManagerDelegate {
    private let locationManager = CLLocationManager()
    private var scanning = false
    private var finished = false

    func applicationDidFinishLaunching(_ notification: Notification) {
        locationManager.delegate = self
        DispatchQueue.main.asyncAfter(deadline: .now() + 60) {
            self.finish(["Error": "Wi-Fi scanning timed out. Allow WiFiSurveyor Scanner in System Settings > Privacy & Security > Location Services."])
        }
        guard CLLocationManager.locationServicesEnabled() else {
            finish(["Error": "Enable Location Services in System Settings > Privacy & Security to read Wi-Fi network names."])
            return
        }
        if locationManager.authorizationStatus == .notDetermined {
            NSApplication.shared.activate(ignoringOtherApps: true)
            // macOS prompts when a location service starts; no coordinates are used.
            locationManager.startUpdatingLocation()
        } else {
            locationManagerDidChangeAuthorization(locationManager)
        }
    }

    func locationManagerDidChangeAuthorization(_ manager: CLLocationManager) {
        guard manager.authorizationStatus != .notDetermined else { return }
        manager.stopUpdatingLocation()
        switch manager.authorizationStatus {
        case .authorizedAlways, .authorizedWhenInUse:
            guard !scanning && !finished else { return }
            scanning = true
            DispatchQueue.global().async { self.scan() }
        default:
            finish(["Error": "Allow WiFiSurveyor Scanner in System Settings > Privacy & Security > Location Services to read Wi-Fi network names."])
        }
    }

    private func scan() {
        let interfaces = CWWiFiClient.shared().interfaces() ?? []
        let poweredInterfaces = interfaces.filter { $0.powerOn() }
        var output: [String: Any]
        if interfaces.isEmpty {
            output = ["Error": "No Wi-Fi interface was found."]
        } else if poweredInterfaces.isEmpty {
            output = ["Error": "Wi-Fi is turned off."]
        } else {
            do {
                var signals: [[String: Any]] = []
                for interface in poweredInterfaces {
                    for network in try interface.scanForNetworks(withSSID: nil) {
                        guard let channel = network.wlanChannel else { continue }
                        let frequency: Int
                        switch channel.channelBand {
                        case .band2GHz: frequency = 2
                        case .band5GHz: frequency = 5
                        default: continue
                        }
                        signals.append([
                            "SSID": network.ssid ?? "",
                            "MAC": network.bssid ?? "",
                            "Frequency": frequency,
                            "Channel": channel.channelNumber,
                            "Strength": network.rssiValue
                        ])
                    }
                }
                output = ["Signals": signals]
            } catch {
                output = ["Error": error.localizedDescription]
            }
        }
        DispatchQueue.main.async { self.finish(output) }
    }

    private func finish(_ output: [String: Any]) {
        guard !finished else { return }
        finished = true
        locationManager.stopUpdatingLocation()
        if let data = try? JSONSerialization.data(withJSONObject: output, options: [.sortedKeys]) {
            FileHandle.standardOutput.write(data)
        }
        NSApplication.shared.terminate(nil)
    }
}

let app = NSApplication.shared
let scanner = Scanner()
app.delegate = scanner
app.setActivationPolicy(.accessory)
app.run()
