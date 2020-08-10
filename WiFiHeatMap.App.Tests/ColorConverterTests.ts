import { Test, TestSuite } from 'xunit.ts';
import ColorConverter from '../WiFiHeatMap.App/ColorConverter';
import Color from '../WiFiHeatMap.App/Color';

export default class ColorConverterTests extends TestSuite
{
    @Test()
    async goodSignalIsGreen(): Promise<void> {
        //arrange
        const dBm = -32;

        //act
        const color = ColorConverter.toColor(dBm);

        //assert
        this.assert.equal(new Color(0, 255, 0).toRGB(), color.toRGB());
    }

    @Test()
    async mediumSignalIsYellow(): Promise<void> {
        //arrange
        const dBm = -64;

        //act
        const color = ColorConverter.toColor(dBm);

        //assert
        this.assert.equal(new Color(255, 255, 0).toRGB(), color.toRGB());
    }

    @Test()
    async BadSignalIsRed(): Promise<void> {
        //arrange
        const dBm = -80;

        //act
        const color = ColorConverter.toColor(dBm);

        //assert
        this.assert.equal(new Color(255, 0, 0).toRGB(), color.toRGB());
    }
}