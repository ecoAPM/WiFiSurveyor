import {Test, TestSuite} from "xunit.ts";
import ImageLoader from "../App/ImageLoader";

export default class ImageLoaderTests extends TestSuite {
	@Test()
	async canLoadImage() {
		//arrange
		const reader = new FileReader();
		const loader = new ImageLoader(reader);

		const blob = ["abc123"];
		const file = new File(blob, "test.txt", { type: "text/plain" });

		//act
		const base64 = await loader.loadImage(file);

		//assert
		this.assert.equal("data:text/plain;base64,YWJjMTIz", base64);
	}
}