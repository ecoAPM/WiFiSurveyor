import {Test, TestSuite} from "xunit.ts";
import FileLoader from "../App/FileLoader";
const jsdom = require("jsdom");
const { JSDOM } = jsdom;
const { window } = new JSDOM();

export default class FileLoaderTests extends TestSuite {
	@Test()
	async canLoadDataURL() {
		//arrange
		const reader = new FileReader();
		const loader = new FileLoader(reader);

		const blob = ["abc123"];
		const file = new window.File(blob, "test.txt", { type: "text/plain" });

		//act
		const base64 = await loader.loadData(file);

		//assert
		this.assert.equal("data:text/plain;base64,YWJjMTIz", base64);
	}

	@Test()
	async canLoadJSON() {
		//arrange
		const reader = new FileReader();
		const loader = new FileLoader(reader);

		const blob = ["{\"abc\":123}"];
		const file = new window.File(blob, "test.json", { type: "application/json" });

		//act
		const json = await loader.loadJSON(file) as { abc: number };

		//assert
		this.assert.equal(123, json.abc);
	}
}