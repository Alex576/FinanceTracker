import { MonoTypeOperatorFunction, shareReplay } from "rxjs";

export function shareLazy<T>(): MonoTypeOperatorFunction<T> {
    return shareReplay<T>({ bufferSize: 1, refCount: true });
}
