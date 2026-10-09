import CoreWLAN
import Foundation

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
                    "SSID": network.ssid ?? "<redacted>",
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

var data = try JSONSerialization.data(withJSONObject: output, options: [.sortedKeys])
data.append(0x0A)
FileHandle.standardOutput.write(data)
