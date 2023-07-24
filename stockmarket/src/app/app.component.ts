import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'stocks';
  constructor(
    public translate: TranslateService, // inject TranslateService
  ) {
    translate.addLangs(['en_US', 'ar_EG']); // add languages
    translate.setDefaultLang('en_US'); // set default language
  }

  switchLang(lang: string) {
    this.translate.use(lang);
  }

  public rtl: boolean = false;

  ngOnInit() {}
}
