import { HubConnection } from "@microsoft/signalr";
import Message from "./Message";
import Signal from "./Signal";

export default class SignalService {
	private static readonly error_message = "Could not connect to server, please try restarting";
	readonly signals: Signal[];
	status: string = "Connecting...";
	last_updated: string = "";

	constructor(connection: HubConnection, signals: Signal[]) {
		this.signals = signals;

		connection.onreconnecting(() => this.status = "Reconnecting...");
		connection.onreconnected(() => this.status = "");
		connection.onclose(() => this.status = SignalService.error_message);

		connection.on("Update", (message: Message) => this.update(message));

		connection.start()
			.then(() => this.status = "")
			.catch(() => this.status = SignalService.error_message);
	}

	update(message: Message): void {
		this.last_updated = message.lastUpdated;
		this.status = message.status;

		this.signals.splice(0);
		message.signals.forEach(data => {
			const signal = new Signal(data.mac, data.ssid, data.frequency, data.strength);
			this.signals.push(signal);
		});
	}
}