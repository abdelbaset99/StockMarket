import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private _isDarkTheme: boolean = false;

  get isDarkTheme(): boolean {
    return this._isDarkTheme;
  }

  set isDarkTheme(value: boolean) {
    this._isDarkTheme = value;
  }

  toggleTheme(): void {
    this._isDarkTheme = !this._isDarkTheme;
    const themeHref = this.isDarkTheme ? 'styles-dark.css' : 'styles.css';
    const linkElement = document.querySelector(
      'link[rel="stylesheet"]'
    ) as HTMLLinkElement;
    linkElement.href = themeHref;
  }

  constructor() {}
}
