import { HubConnectionBuilder } from "@microsoft/signalr";

import Renderer from "./Renderer";
import Signal from "./Signal";
import SignalService from "./SignalService";
import BackgroundParser from "./BackgroundParser";

export default class Factory {

	static backgroundParser(): BackgroundParser {
		const fileReader = new FileReader();
		return new BackgroundParser(fileReader);
	}

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