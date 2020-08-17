import SignalService from "./SignalService";
import { HubConnectionBuilder } from "@microsoft/signalr";
import Renderer from "./Renderer";
import Signal from "./Signal";

export default class Factory {
    static renderer(canvas: HTMLCanvasElement) {
        return new Renderer(canvas)
    };

    static signalService(signals: Signal[]): SignalService {
        const connection = new HubConnectionBuilder().withUrl(`${process.env.NODE_ENV === 'production' ? '' : 'http://localhost:5001'}/signals`).build();
        return new SignalService(connection, signals);
    }
}