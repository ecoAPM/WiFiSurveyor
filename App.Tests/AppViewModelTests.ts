import { Test, TestSuite } from "xunit.ts";
import AppViewModel from "../App/AppViewModel";
import Mockito from "ts-mockito";
import ImageLoader from "../App/ImageLoader";

export default class AppViewModelTests extends TestSuite
{
	@Test()
	async canSetBackgroundFromImageLoaderData() {
		//arrange
		const file = Mockito.mock<File>();

		const loader = Mockito.mock<ImageLoader>();
		Mockito.when(loader.loadImage(file)).thenResolve("data:image/png;base64,abc123");
		const vm = new AppViewModel();
		vm.image_loader = Mockito.instance(loader);

		const files = Mockito.mock<FileList>();
		Mockito.when(files.length).thenReturn(1);
		Mockito.when(files.item(0)).thenReturn(file);

		//act
		await vm.setBackground(Mockito.instance(files));

		//assert
		this.assert.equal("data:image/png;base64,abc123", vm.background);
	}
}