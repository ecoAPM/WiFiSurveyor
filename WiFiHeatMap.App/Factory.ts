import SignalService from "./SignalService";
import { HubConnectionBuilder } from "@microsoft/signalr";
import Renderer from "./Renderer";
import Signal from "./Signal";

export default class Factory {
    static renderer(canvas: HTMLCanvasElement) {
        return new Renderer(canvas)
    };

    static signalService(signals: Signal[]): SignalService {
        const server = process.env.NODE_ENV === 'production' ? '' : 'http://localhost:5001';

        const connection = new HubConnectionBuilder()
            .withUrl(`${server}/signals`)
            .withAutomaticReconnect()
            .build();

        return new SignalService(connection, signals);
    }
}