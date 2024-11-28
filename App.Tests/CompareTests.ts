import { Test, TestSuite } from "xunit.ts";
import Compare from "../App/Compare";

export default class CompareTests extends TestSuite {
	@Test()
	async canCompareNumbers() {
		//arrange
		const number1 = 1;
		const number2 = 2;
		const number1again = 1;

		//act
		const less = Compare.numbers(number1, number2);
		const greater = Compare.numbers(number2, number1);
		const equal = Compare.numbers(number1, number1again);

		//assert
		this.assert.equal(-1, less);
		this.assert.equal(1, greater);
		this.assert.equal(0, equal);
	}

	@Test()
	async canCompareStrings() {
		//arrange
		const string1 = "test1";
		const string2 = "test2";
		const string1again = "test1";

		//act
		const less = Compare.strings(string1, string2);
		const greater = Compare.strings(string2, string1);
		const equal = Compare.strings(string1, string1again);

		//assert
		this.assert.equal(-1, less);
		this.assert.equal(1, greater);
		this.assert.equal(0, equal);
	}
}