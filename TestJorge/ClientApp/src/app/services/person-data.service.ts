import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../models/person.model';

@Injectable({
  providedIn: 'root'
})
export class PersonDataService {

  private readonly baseUrl = `${window.location.origin}/api/person/`;
  constructor(private client: HttpClient) { }

  public getPeople(): Observable<Array<Person>> {
    const url = `${this.baseUrl}getPeople`;
    return this.client.get<Array<Person>>(url);
  }
}
