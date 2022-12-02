export default class Color {
	red: number = 0;
	green: number = 0;
	blue: number = 0;
	alpha: number = 0;

	constructor(red: number, green: number, blue: number, alpha: number = 255) {
		this.red = ~~Color.clamp(red);
		this.green = ~~Color.clamp(green);
		this.blue = ~~Color.clamp(blue);
		this.alpha = ~~Color.clamp(alpha);
	}

	private static clamp(value: number): number {
		return Math.min(Math.max(value, 0), 255);
	}

	toRGB(): string {
		return `rgb(${this.red}, ${this.green}, ${this.blue})`;
	}

	toRGBA(): string {
		return `rgba(${this.red}, ${this.green}, ${this.blue}, ${(this.alpha > 0 ? this.alpha + 1 : this.alpha) / 256})`;
	}

	toHEX(): string {
		return `#${this.red.toString(16)}${this.green.toString(16)}${this.blue.toString(16)}`;
	}

	toHEXA(): string {
		return `#${this.red.toString(16)}${this.green.toString(16)}${this.blue.toString(16)}${this.alpha.toString(16)}`;
	}
}