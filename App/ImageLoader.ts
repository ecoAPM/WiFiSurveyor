export default class ImageLoader {

	private readonly file_reader: FileReader;

	constructor(file_reader: FileReader) {
		this.file_reader = file_reader;
	}

	async loadImage(file: Blob) {
		return new Promise<string>((resolve, reject) => {
			this.file_reader.onerror = () => reject(new DOMException("Could not read file"));
			this.file_reader.onload = () => resolve(this.file_reader.result as string);
			this.file_reader.readAsDataURL(file);
		});
	}
}