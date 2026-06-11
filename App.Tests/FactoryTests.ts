import Mockito from "ts-mockito";
import { Test, TestSuite } from "xunit.ts";

import Factory from "../App/Factory";
import Renderer from "../App/Renderer";
import Signal from "../App/Signal";
import SignalService from "../App/SignalService";
import MockFactory from "./MockFactory";

export default class FactoryTests extends TestSuite {
	@Test()
	canCreateSignalService() {
		//arrange
		const signals: Signal[] = [];

		//act
		const signal_service = Factory.signalService("http://localhost", signals);

		//assert
		this.assert.instanceOf(SignalService, signal_service);
	}

	@Test()
	canCreateRenderer() {
		//arrange
		const canvas = MockFactory.canvas();

		//act
		const renderer = Factory.renderer(Mockito.instance(canvas));

		//assert
		this.assert.instanceOf(Renderer, renderer);
	}
}
