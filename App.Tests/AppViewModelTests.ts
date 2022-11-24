import {Test, TestSuite} from "xunit.ts";
import AppViewModel from "../App/AppViewModel";
import Mockito from "ts-mockito";
import FileLoader from "../App/FileLoader";

export default class AppViewModelTests extends TestSuite {
	@Test()
	async canSetBackgroundFromFileLoaderData() {
		//arrange
		const file = Mockito.mock<File>();

		const loader = Mockito.mock<FileLoader>();
		Mockito.when(loader.loadData(file)).thenResolve("data:image/png;base64,abc123");
		const vm = new AppViewModel();
		vm.file_loader = Mockito.instance(loader);

		const files = Mockito.mock<FileList>();
		Mockito.when(files.length).thenReturn(1);
		Mockito.when(files.item(0)).thenReturn(file);

		//act
		await vm.setBackground(Mockito.instance(files));

		//assert
		this.assert.equal("data:image/png;base64,abc123", vm.background);
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
		const file = Mockito.mock<File>();

		const loader = Mockito.mock<FileLoader>();
		Mockito.when(loader.loadJSON(file)).thenResolve(this.data);
		const vm = new AppViewModel();
		vm.file_loader = Mockito.instance(loader);

		const files = Mockito.mock<FileList>();
		Mockito.when(files.length).thenReturn(1);
		Mockito.when(files.item(0)).thenReturn(file);

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