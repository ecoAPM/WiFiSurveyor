import SignalService from "./SignalService";
import Renderer from "./Renderer";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import Point from "./Point";

export default class AppViewModel {
    signal_service: SignalService | null = null;
    renderer: Renderer | null = null;
    background: string = '';
    pixelated: boolean = false;
    readings: Reading[] = [];
    selected: AccessPoint | null = null;
    current: Reading = new Reading(0, new Point(0, 0), []);
    debug: boolean = false;
}