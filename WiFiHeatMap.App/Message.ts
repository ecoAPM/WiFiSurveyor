import Signal from "./Signal";

export default class Message {
    status: string;
    signals: Signal[];
    last_updated: Date;
}