export default class Compare {
	static numbers(n1: number, n2: number): number {
		return this.base(n1, n2);
	}

	static strings(s1: string, s2: string): number {
		return this.base(s1, s2);
	}

	private static base(n1: any, n2: any) {
		if (n1 < n2) {
			return -1;
		}

		if (n1 > n2) {
			return 1;
		}

		return 0;
	}
}