import { Test, TestSuite } from "xunit.ts";
import Signal from "../WiFiHeatMap.App/Signal";

export default class SignalTests extends TestSuite {
    @Test()
    async canCreateSignal() {
        //arrange
        const mac = '12:34:56:78:90:ab', ssid = 'test', frequency = 2, strength = -50;

        //act
        const signal = new Signal(mac, ssid, frequency, strength);

        //assert
        this.assert.equal('12:34:56:78:90:ab', signal.mac);
        this.assert.equal('test', signal.ssid);
        this.assert.equal(2, signal.frequency);
        this.assert.equal(-50, signal.strength);
    }
}