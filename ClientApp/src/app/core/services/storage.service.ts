import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  public saveValue(key: string, value: string): void {
    localStorage.setItem(key, value);
  }

  public getValue<T = string>(key: string): T {
    const savedValue = localStorage.getItem(key);
    try {
      const value = JSON.parse(savedValue);
      return value as T;
    } catch {
      return savedValue as T;
    }
  }

  public remove(key: string): void {
    localStorage.removeItem(key);
  }
}
