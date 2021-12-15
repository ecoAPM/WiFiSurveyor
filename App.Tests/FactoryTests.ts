import { Test, TestSuite } from "xunit.ts";
import Factory from "../App/Factory";
import Signal from "../App/Signal";
import SignalService from "../App/SignalService";
import MockFactory from "./MockFactory";
import Mockito from "ts-mockito";
import Renderer from "../App/Renderer";
import ImageLoader from "../App/ImageLoader";

export default class FactoryTests extends TestSuite {
	@Test()
	async canCreateSignalService() {
		//arrange
		const signals: Signal[] = [];

		const factory = new Factory(false);

		//act
		const signal_service = factory.signalService(signals);

		//assert
		this.assert.instanceOf(SignalService, signal_service);
	}

	@Test()
	async canCreateRenderer() {
		//arrange
		const canvas = MockFactory.canvas();

		const factory = new Factory(true);

		//act
		const renderer = factory.renderer(Mockito.instance(canvas));

		//assert
		this.assert.instanceOf(Renderer, renderer);
	}

	@Test()
	async canCreateImageLoader() {
		//arrange
		const factory = new Factory(true);

		//act
		const loader = factory.imageLoader();

		//assert
		this.assert.instanceOf(ImageLoader, loader);
	}
}
