import {Test, TestSuite} from 'xunit.ts';
import Compare from '../App/Compare';

export default class CompareTests extends TestSuite {
	@Test()
	async canCompareNumbers() {
		//arrange
		const n1 = 1;
		const n2 = 2;
		const n1again = 1;

		//act
		const less = Compare.numbers(n1, n2);
		const greater = Compare.numbers(n2, n1);
		const equal = Compare.numbers(n1, n1again);

		//assert
		this.assert.equal(-1, less);
		this.assert.equal(1, greater);
		this.assert.equal(0, equal);
	}

	@Test()
	async canCompareStrings() {
		//arrange
		const s1 = 'test1';
		const s2 = 'test2';
		const s1again = 'test1';

		//act
		const less = Compare.strings(s1, s2);
		const greater = Compare.strings(s2, s1);
		const equal = Compare.strings(s1, s1again);

		//assert
		this.assert.equal(-1, less);
		this.assert.equal(1, greater);
		this.assert.equal(0, equal);
	}
}