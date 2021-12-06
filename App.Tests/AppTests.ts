import { Test, TestSuite } from "xunit.ts";
import App from "../App/app.vue";
import { shallowMount as mount } from "@vue/test-utils";
import Mockito from "ts-mockito";
import Renderer from "../App/Renderer";
import MockFactory from "./MockFactory";

export default class AppTests extends TestSuite {
	@Test()
	async canCreateApp() {
		//arrange
		const signal_service = MockFactory.signalService();

		const canvas = MockFactory.canvas();
		const renderer = new Renderer(Mockito.instance(canvas));

		//act
		const component = mount(App, {
			provide: {
				signal_service: () => Mockito.instance(signal_service),
				renderer: () => renderer
			}
		});

		//assert
		this.assert.notEmpty(component.html());
	}

	@Test()
	async canGetStatusFromSignalService() {
		//arrange
		const signal_service = MockFactory.signalService();
		Mockito.when(signal_service.status).thenReturn("test message");

		const canvas = MockFactory.canvas();
		const renderer = new Renderer(Mockito.instance(canvas));

		//act
		const component = mount(App, {
			provide: {
				signal_service: () => Mockito.instance(signal_service),
				renderer: () => renderer
			}
		});
		await component.vm.$nextTick();

		//assert
		this.assert.stringContains("test message", component.html());
	}

	@Test()
	async showsDefaultStatusWhenLoading() {
		//arrange
		const canvas = MockFactory.canvas();
		const renderer = new Renderer(Mockito.instance(canvas));

		//act
		const component = mount(App, { provide: { signal_service: () => null, renderer: () => renderer } });

		//assert
		this.assert.stringContains("loading", component.vm.connection_status);
	}
}