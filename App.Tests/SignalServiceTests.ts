import { Test, TestSuite } from "xunit.ts";
import SignalService from "../App/SignalService";
import Signal from "../App/Signal";
import Message from "../App/Message";
import { HubConnection } from "@microsoft/signalr";
import Mockito from "ts-mockito";

export default class SignalServiceTests extends TestSuite {
	@Test()
	async startConnectsToHub() {
		//arrange
		const connection = Mockito.mock<HubConnection>();
		const signals: Signal[] = [];
		const service = new SignalService(Mockito.instance(connection), signals);

		//act
		await service.start();

		//assert
		Mockito.verify(connection.start()).once();
	}

	@Test()
	async signalsAreUpdatedOnNewMessage() {
		//arrange
		const connection = Mockito.mock<HubConnection>();
		Mockito.when(connection.start()).thenResolve();

		const signal1 = new Signal("mac1", "ssid1", 2, 1, -50);
		const signal2 = new Signal("mac2", "ssid2", 2, 1, -40);
		const signals = [signal1, signal2];
		const service = new SignalService(Mockito.instance(connection), signals);

		const message = new Message();
		const new1 = new Signal("mac1", "ssid1", 2, 1, -45);
		const new2 = new Signal("mac2", "ssid2", 2, 1, -44);
		message.signals = [new1, new2];

		//act
		service.update(message);

		//assert
		this.assert.equal(-45, signals[0].strength);
		this.assert.equal(-44, signals[1].strength);
	}
}