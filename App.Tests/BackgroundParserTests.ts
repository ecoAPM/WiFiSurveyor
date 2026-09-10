import Mockito from "ts-mockito";
import { Test, TestSuite } from "xunit.ts";

import BackgroundParser from "../App/BackgroundParser";

export default class BackgroundParserTests extends TestSuite {
	@Test()
	async canLoadDataFromFile() {
		//arrange
		const file = new Blob(["abc123"], { type: "image/png" });
		const mockReader = Mockito.mock<FileReader>();
		const fileReader = Mockito.instance(mockReader);

		const parser = new BackgroundParser(fileReader);

		//act
		const promise = parser.read(file);

		const e = { target: { result: "data:image/png;base64,YWJjMTIz" } } as ProgressEvent<FileReader>;
		if (fileReader.onload)
			fileReader.onload(e);

		const data = await promise;

		//assert
		this.assert.equal("data:image/png;base64,YWJjMTIz", data);
	}
}