import SignalService from "./SignalService";
import Renderer from "./Renderer";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import Point from "./Point";
import AccessPointGrouping from "./AccessPointGrouping";

export default class AppViewModel {
	signal_service: SignalService | null = null;
	renderer: Renderer | null = null;
	background: string = "";
	pixelated: boolean = true;
	readings: Reading[] = [];
	selected: AccessPoint | null = null;
	current: Reading = new Reading(0, new Point(0, 0), []);
	group_by: AccessPointGrouping = new AccessPointGrouping();
	debug: boolean = false;

	async setBackground(files: FileList): Promise<void> {
		if (files.length !== 1) {
			this.background = "";
			return;
		}

		const file = files.item(0) as File;
		const reader = new FileReader();
		const file_contents = new Promise<string>((resolve, reject) => {
			reader.onerror = () => reject(new DOMException("sad face"));
			reader.onload = () => resolve(reader.result as string);
			reader.readAsDataURL(file);
		});

		this.background = await file_contents;
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