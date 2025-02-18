import { Test, TestSuite } from "xunit.ts";
import Color from "../App/Color";

export default class ColorTests extends TestSuite {
	@Test()
	async canCreateColor() {
		//arrange
		const red = 12, green = 23, blue = 34, alpha = 45;

		//act
		const color = new Color(red, green, blue, alpha);

		//assert
		this.assert.equal(12, color.red);
		this.assert.equal(23, color.green);
		this.assert.equal(34, color.blue);
		this.assert.equal(45, color.alpha);
	}

	@Test()
	async componentsAreClamped() {
		//arrange
		const too_low = -1;
		const too_high = 256;

		//act
		const color = new Color(too_low, too_high, too_low, too_high);

		//assert
		this.assert.equal(0, color.red);
		this.assert.equal(255, color.green);
		this.assert.equal(0, color.blue);
		this.assert.equal(255, color.alpha);
	}

	@Test()
	async componentsAreFloored() {
		//arrange
		const low = 127.25;
		const mid = 127.5;
		const high = 127.75;

		//act
		const color = new Color(low, mid, high, mid);

		//assert
		this.assert.equal(127, color.red);
		this.assert.equal(127, color.green);
		this.assert.equal(127, color.blue);
		this.assert.equal(127, color.alpha);
	}

	@Test()
	async canConvertToRGB() {
		//arrange
		const color = new Color(12, 23, 34);

		//act
		const rgb_css_string = color.toRGB();

		//assert
		this.assert.equal("rgb(12, 23, 34)", rgb_css_string);
	}

	@Test()
	async canConvertToRGBA() {
		//arrange
		const color = new Color(12, 23, 34, 63);

		//act
		const rgba_css_string = color.toRGBA();

		//assert
		this.assert.equal("rgba(12, 23, 34, 0.25)", rgba_css_string);
	}

	@Test()
	async canConvertAlphaFloatCorrectly() {
		//act
		const clear = new Color(12, 23, 34, 0).toRGBA();
		const half = new Color(12, 23, 34, 127).toRGBA();
		const full = new Color(12, 23, 34, 255).toRGBA();

		//assert
		this.assert.equal("rgba(12, 23, 34, 0)", clear);
		this.assert.equal("rgba(12, 23, 34, 0.5)", half);
		this.assert.equal("rgba(12, 23, 34, 1)", full);
	}

	@Test()
	async canConvertToHEX() {
		//arrange
		const color = new Color(127, 63, 255);

		//act
		const hex_css_string = color.toHEX();

		//assert
		this.assert.equal("#7f3fff", hex_css_string);
	}

	@Test()
	async canConvertToHEXA() {
		//arrange
		const color = new Color(127, 63, 31);

		//act
		const hex_css_string = color.toHEXA();

		//assert
		this.assert.equal("#7f3f1fff", hex_css_string);
	}

	@Test()
	async canConvertBlackToHEXA() {
		//arrange
		const color = new Color(0, 0, 0);

		//act
		const hex_css_string = color.toHEXA();

		//assert
		this.assert.equal("#000000ff", hex_css_string);
	}
}