import { HubConnection } from "@microsoft/signalr";
import Message from "./Message";
import Signal from "./Signal";

export default class SignalService {
	private static readonly error_message = "Could not connect to server, please try restarting";
	private readonly connection: HubConnection;
	readonly signals: Signal[];
	status: string = "Connecting...";
	last_updated: string = "";

	constructor(connection: HubConnection, signals: Signal[]) {
		this.connection = connection;
		this.signals = signals;
	}

	async start() {
		this.connection.onreconnecting(() => this.status = "Reconnecting...");
		this.connection.onreconnected(() => this.status = "");
		this.connection.onclose(() => this.status = SignalService.error_message);

		this.connection.on("Update", (message: Message) => this.update(message));

		try {
			await this.connection.start();
			this.status = "";
		}
		catch {
			this.status = SignalService.error_message;
		}
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