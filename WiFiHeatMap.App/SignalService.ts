import { HubConnection } from '@microsoft/signalr';
import Message from './Message';
import Signal from './Signal';

export default class SignalService {
    readonly signals: Signal[]

    constructor(connection: HubConnection, signals: Signal[]) {
        this.signals = signals;
        connection.on("Update", (message: Message) => this.update(message));
        connection.start();
    }

    update(message: Message): void {
        this.signals.splice(0, this.signals.length);
        message.signals.forEach(signal => this.signals.push(signal));
    }
}