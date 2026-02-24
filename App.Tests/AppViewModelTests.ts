import {Test, TestSuite} from "xunit.ts";
import AppViewModel from "../App/AppViewModel";
import Mockito from "ts-mockito";

export default class AppViewModelTests extends TestSuite {
	@Test()
	async canSetBackgroundFromFileData() {
		//arrange
		const data = new TextEncoder().encode("abc123");

		const mockFile = Mockito.mock<File>();
		Mockito.when(mockFile.type).thenReturn("image/png")
		Mockito.when(mockFile.arrayBuffer()).thenResolve(data.buffer);
		const file = Mockito.instance(mockFile);

		const files = Mockito.mock<FileList>();
		Mockito.when(files.length).thenReturn(1);
		Mockito.when(files.item(0)).thenReturn(file);

		const vm = new AppViewModel();

		//act
		await vm.setBackground(Mockito.instance(files));

		//assert
		this.assert.equal("data:image/png;base64,YWJjMTIz", vm.background);
	}

	private readonly data: object = {
		name: "Test",
		readings: [
			{
				id: 1,
				location: {x: 123, y: 234},
				signals: [
					{
						ssid: "Wi-Fi",
						mac: "ab-cd-ef-12-34",
						frequency: 2,
						channel: 1,
						strength: -64
					}
				]
			}
		]
	};

	@Test()
	async canLoadPreviouslySavedData() {
		//arrange
		const json = JSON.stringify(this.data);
		const mockFile = Mockito.mock<File>();
		Mockito.when(mockFile.text()).thenResolve(json);
		const file = Mockito.instance(mockFile);

		const files = Mockito.mock<FileList>();
		Mockito.when(files.length).thenReturn(1);
		Mockito.when(files.item(0)).thenReturn(file);

		const vm = new AppViewModel();

		//act
		await vm.load(Mockito.instance(files));

		//assert
		this.assert.equal("Test", vm.name);
		this.assert.equal(123, vm.readings[0].location.x);
		this.assert.equal(234, vm.readings[0].location.y);
		this.assert.equal("Wi-Fi", vm.readings[0].signals[0].ssid);
		this.assert.equal("ab-cd-ef-12-34", vm.readings[0].signals[0].mac);
		this.assert.equal(2, vm.readings[0].signals[0].frequency);
		this.assert.equal(1, vm.readings[0].signals[0].channel);
		this.assert.equal(-64, vm.readings[0].signals[0].strength);
	}
}