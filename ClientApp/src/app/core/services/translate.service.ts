import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { TranslateModel } from '../models/translate-model';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class TranslateService extends BaseApiService {
  private readonly translationMap = new Map<string, string>();

  loadTranslationsAsync(languageCode: LanguageCode): Observable<void> {
    return this.post<TranslateModel[]>('Translation/GetTranslations', languageCode)
      .pipe(
        map((translations) => translations.forEach(model => this.translationMap.set(model.key, model.value)))
      );
  }

  translate(key: unknown): string {
    const fullKey = `${key}`;
    return this.translationMap.get(fullKey) || fullKey;
  }
}

export enum LanguageCode {
  EN = 1,
}
