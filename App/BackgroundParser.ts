export default class BackgroundParser {
	constructor(private readonly fileReader: FileReader) {
	}

	async read(file: Blob): Promise<string> {
		return new Promise((resolve, reject) => {
			function handleSuccess(event: ProgressEvent<FileReader>) {
				const data = event.target?.result as string;
				resolve(data);
			}

			function handleFailure(event: ProgressEvent<FileReader>){
				const error = event as unknown as Error;
				reject(error);
			}

			this.fileReader.onload = handleSuccess;
			this.fileReader.onerror = handleFailure;
			this.fileReader.readAsDataURL(file);
		});
	}
}