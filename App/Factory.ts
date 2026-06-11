import { HubConnectionBuilder } from "@microsoft/signalr";

import Renderer from "./Renderer";
import Signal from "./Signal";
import SignalService from "./SignalService";

export default class Factory {

	static signalService(server: string, signals: Signal[]): SignalService {
		const connection = new HubConnectionBuilder()
			.withUrl(`${server}/signals`)
			.withAutomaticReconnect()
			.build();

		return new SignalService(connection, signals);
	}

	static renderer(canvas: HTMLCanvasElement): Renderer {
		return new Renderer(canvas);
	}
}