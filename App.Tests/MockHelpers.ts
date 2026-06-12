import Mockito from "ts-mockito";

export function any<T>(val?: T): T {
	return val ?? Mockito.anything() as T;
}