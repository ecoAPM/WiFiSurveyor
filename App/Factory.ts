import SignalService from "./SignalService";
import {HubConnectionBuilder} from "@microsoft/signalr";
import Renderer from "./Renderer";
import Signal from "./Signal";
import ImageLoader from "./ImageLoader";

export default class Factory {
	
	private readonly server: string;

	constructor(is_local: boolean) {
		this.server = is_local ? "" : "http://localhost:5000";
	}

	signalService(signals: Signal[]): SignalService {
		const connection = new HubConnectionBuilder()
			.withUrl(`${this.server}/signals`)
			.withAutomaticReconnect()
			.build();

		return new SignalService(connection, signals);
	}

	renderer(canvas: HTMLCanvasElement): Renderer {
		return new Renderer(canvas);
	}

	imageLoader(): ImageLoader {
		const file_reader = new FileReader();
		return new ImageLoader(file_reader);
	}
}