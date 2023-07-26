import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ThemeService } from './theme.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css', '../styles.css', '../styles-dark.css'],
})
export class AppComponent {
  title = 'stocks';
  constructor(
    public translate: TranslateService, // inject TranslateService
    public themeService: ThemeService
  ) {
    translate.addLangs(['ar_EG', 'en_US']); // add languages
    translate.setDefaultLang('ar_EG'); // set default language
  }

  switchLang(lang: string) {
    this.translate.use(lang);
  }

  toggleLang(){
    this.translate.use(this.translate.currentLang === 'ar_EG' ? 'en_US' : 'ar_EG');
  }

  toggleTheme() {
    this.themeService.toggleTheme();
  }

  ngOnInit() {}
}
