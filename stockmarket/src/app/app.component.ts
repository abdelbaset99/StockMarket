import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ThemeService } from './theme.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'stocks';
  constructor(
    public translate: TranslateService, // inject TranslateService
    private themeService: ThemeService
  )
  {
    translate.addLangs(['en_US', 'ar_EG']); // add languages
    translate.setDefaultLang('en_US'); // set default language
  }

  switchLang(lang: string) {
    this.translate.use(lang);
  }

  toggleTheme() {
    this.themeService.toggleTheme();
  }

  ngOnInit() {}
}
