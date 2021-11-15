import { Test, TestSuite } from "xunit.ts";
import AccessPoint from "../App/AccessPoint";

export default class AccessPointTests extends TestSuite
{
    @Test()
    async canCreateAccessPoint() {
        //arrange
        const ssid = 'test', frequency = 2, mac = 'ab:cd:ef';

        //act
        const ap = new AccessPoint(ssid, frequency, mac);

        //assert
        this.assert.equal('test', ap.ssid);
        this.assert.equal(2, ap.frequency);
        this.assert.equal('ab:cd:ef', ap.mac);
    }

    @Test()
    async canCreateAggregateAP() {
        //arrange
        const ssid = 'test';

        //act
        const ap = new AccessPoint(ssid);

        //assert
        this.assert.equal('test', ap.ssid);
        this.assert.null(ap.frequency);
        this.assert.null(ap.mac);
    }

    @Test()
    async canGetLabel() {
        //arrange
        const ap = new AccessPoint('test', 2, 'ab:cd:ef');

        //act
        const label = ap.label();

        //assert
        this.assert.equal('test @ 2 GHz (ab:cd:ef)', label);
    }

    @Test()
    async canGetFrequencyGroupedLabel() {
        //arrange
        const ap = new AccessPoint('test', 2);

        //act
        const label = ap.label();

        //assert
        this.assert.equal('test @ 2 GHz', label);
    }

    @Test()
    async canGetSSIDGroupedLabel() {
        //arrange
        const ap = new AccessPoint('test');

        //act
        const label = ap.label();

        //assert
        this.assert.equal('test', label);
    }

	@Test()
	async canCompareLabels()
	{
		//arrange
		const ap1 = new AccessPoint('test1');
		const ap2 = new AccessPoint('test2');
		const ap1again = new AccessPoint('test1');

		//act
		const less = ap1.compareTo(ap2);
		const greater = ap2.compareTo(ap1);
		const equal = ap1.compareTo(ap1again);

		//assert
		this.assert.equal(-1, less);
		this.assert.equal(1, greater);
		this.assert.equal(0, equal);
	}
}