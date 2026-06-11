import { Test, TestSuite } from "xunit.ts";

import Color from "../App/Color";
import ColorConverter from "../App/ColorConverter";

export default class ColorConverterTests extends TestSuite {
	@Test()
	goodSignalIsGreen() {
		//arrange
		const dBm = -40;

		//act
		const color = ColorConverter.fromSignal(dBm);

		//assert
		this.assert.equal(new Color(0, 255, 0).toRGB(), color.toRGB());
	}

	@Test()
	mediumSignalIsYellow() {
		//arrange
		const dBm = -60;

		//act
		const color = ColorConverter.fromSignal(dBm);

		//assert
		this.assert.equal(new Color(255, 255, 0).toRGB(), color.toRGB());
	}

	@Test()
	BadSignalIsRed() {
		//arrange
		const dBm = -80;

		//act
		const color = ColorConverter.fromSignal(dBm);

		//assert
		this.assert.equal(new Color(255, 0, 0).toRGB(), color.toRGB());
	}

	@Test()
	goodSNRIsGreen() {
		//arrange
		const dB = 50;

		//act
		const color = ColorConverter.fromSNR(dB);

		//assert
		this.assert.equal(new Color(0, 255, 0).toRGB(), color.toRGB());
	}

	@Test()
	mediumSNRIsYellow() {
		//arrange
		const dB = 20;

		//act
		const color = ColorConverter.fromSNR(dB);

		//assert
		this.assert.equal(new Color(255, 255, 0).toRGB(), color.toRGB());
	}

	@Test()
	BadSNRIsRed() {
		//arrange
		const dB = 0;

		//act
		const color = ColorConverter.fromSNR(dB);

		//assert
		this.assert.equal(new Color(255, 0, 0).toRGB(), color.toRGB());
	}
}