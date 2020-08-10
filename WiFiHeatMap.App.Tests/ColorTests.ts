import { Test, TestSuite } from 'xunit.ts';
import Color from '../WiFiHeatMap.App/Color';

export default class ColorTests extends TestSuite
{
    @Test()
    async canCreateColor(): Promise<void> {
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
    async componentsAreClamped(): Promise<void> {
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
    async canConvertToRGB(): Promise<void> {
        //arrange
        const color = new Color(12, 23, 34);

        //act
        const rgb_css_string = color.toRGB();

        //assert
        this.assert.equal('rgb(12,23,34)', rgb_css_string);
    }

    @Test()
    async canConvertToRGBA(): Promise<void> {
        //arrange
        const color = new Color(12, 23, 34, 45);

        //act
        const rgba_css_string = color.toRGBA();

        //assert
        this.assert.equal('rgba(12,23,34,45)', rgba_css_string);
    }

    @Test()
    async canConvertToHEX(): Promise<void> {
        //arrange
        const color = new Color(127, 63, 255);

        //act
        const hex_css_string = color.toHEX();

        //assert
        this.assert.equal('#7f3fff', hex_css_string);
    }

    @Test()
    async canConvertToHEXA(): Promise<void> {
        //arrange
        const color = new Color(127, 63, 31);

        //act
        const hex_css_string = color.toHEXA();

        //assert
        this.assert.equal('#7f3f1fff', hex_css_string);
    }
}