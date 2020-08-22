import { PersonDataService } from './services/person-data.service';
import { Component } from '@angular/core';
import { Person } from './models/person.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  public secret: string;
  public people: Array<Person>;
  constructor(private personService: PersonDataService) {
    this.people = new Array();
  }

  async handler() {
    this.people = await this.personService.getPeople().toPromise();
  }

  async secretHandler() {
    this.secret = (await this.personService.getSecret().toPromise()).secret;
  }
}
