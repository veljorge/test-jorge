import { PersonDataService } from './services/person-data.service';
import { Component } from '@angular/core';
import { Person } from './models/person.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  public people: Array<Person>;
  constructor(private personService: PersonDataService) {
    this.people = new Array();
  }

  handler() {
    this.personService.getPeople().subscribe(x => {
      this.people = x;
    }, error => {
      console.log(error);
    })
  }
}
