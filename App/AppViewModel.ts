import SignalService from "./SignalService";
import Renderer from "./Renderer";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import Point from "./Point";
import AccessPointGrouping from "./AccessPointGrouping";
import FileLoader from "./FileLoader";
import Signal from "./Signal";

export default class AppViewModel {
	name: string = "";
	background: string = "";
	pixelated: boolean = true;
	readings: Reading[] = [];
	selected: AccessPoint | null = null;
	current: Reading = new Reading(0, new Point(0, 0), []);
	group_by: AccessPointGrouping = new AccessPointGrouping();
	debug: boolean = false;

	signal_service: SignalService | null = null;
	renderer: Renderer | null = null;
	file_loader: FileLoader | null = null;

	async load(files: FileList): Promise<void> {
		if (files.length !== 1) {
			return;
		}

		const file = files.item(0) as File;
		const json = await this.file_loader?.loadJSON(file) as AppViewModel;

		this.restore(json);
	}

	private restore(json: AppViewModel) {
		this.name = json.name;
		this.background = json.background;
		this.pixelated = json.pixelated;
		this.selected = json.selected;

		if (json.group_by) {
			this.group_by.ssid = json.group_by.ssid;
			this.group_by.frequency = json.group_by.frequency;
		}

		if (json.readings) {
			this.readings = json.readings.map(r => new Reading(r.id,
				new Point(r.location.x, r.location.y),
				r.signals.map(s => new Signal(s.mac, s.ssid, s.frequency, s.channel, s.strength))));
		}
	}

	async setBackground(files: FileList): Promise<void> {
		if (files.length !== 1) {
			this.background = "";
			return;
		}

		const file = files.item(0) as File;
		this.background = await this.file_loader?.loadData(file) ?? "";
	}

	deleteDataPoint(index: number): void {
		this.readings.splice(index, 1);
		if (this.readings.length >= 3)
			this.renderer?.render(this.readings, this.selected);
		else
			this.renderer?.clear();
	}

	clearAllDataPoints(): void {
		this.readings = [];
		this.renderer?.clear();
	}
}