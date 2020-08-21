/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PersonDataService } from './person-data.service';

describe('Service: PersonData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonDataService]
    });
  });

  it('should ...', inject([PersonDataService], (service: PersonDataService) => {
    expect(service).toBeTruthy();
  }));
});
