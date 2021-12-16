export default class FileLoader {

	private readonly file_reader: FileReader;

	constructor(file_reader: FileReader) {
		this.file_reader = file_reader;
	}

	async loadJSON(file: Blob): Promise<object> {
		return new Promise<object>((resolve, reject) => {
			this.file_reader.onerror = () => reject(new DOMException("Could not read file"));
			this.file_reader.onload = () => resolve(JSON.parse(this.file_reader.result as string));
			this.file_reader.readAsText(file);
		});
	}

	async loadData(file: Blob): Promise<string> {
		return new Promise<string>((resolve, reject) => {
			this.file_reader.onerror = () => reject(new DOMException("Could not read file"));
			this.file_reader.onload = () => resolve(this.file_reader.result as string);
			this.file_reader.readAsDataURL(file);
		});
	}
}