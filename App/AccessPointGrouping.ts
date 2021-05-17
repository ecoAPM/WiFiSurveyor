export default class AccessPointGrouping {
    ssid: boolean = true;
    frequency: boolean = true;

    update(): void {
        if (!this.ssid)
            this.frequency = false;
    }
}