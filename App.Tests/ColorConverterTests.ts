import { Test, TestSuite } from 'xunit.ts';
import ColorConverter from '../App/ColorConverter';
import Color from '../App/Color';

export default class ColorConverterTests extends TestSuite
{
    @Test()
    async goodSignalIsGreen() {
        //arrange
        const dBm = -32;

        //act
        const color = ColorConverter.toColor(dBm);

        //assert
        this.assert.equal(new Color(0, 255, 0).toRGB(), color.toRGB());
    }

    @Test()
    async mediumSignalIsYellow() {
        //arrange
        const dBm = -64;

        //act
        const color = ColorConverter.toColor(dBm);

        //assert
        this.assert.equal(new Color(255, 255, 0).toRGB(), color.toRGB());
    }

    @Test()
    async BadSignalIsRed() {
        //arrange
        const dBm = -80;

        //act
        const color = ColorConverter.toColor(dBm);

        //assert
        this.assert.equal(new Color(255, 0, 0).toRGB(), color.toRGB());
    }
}