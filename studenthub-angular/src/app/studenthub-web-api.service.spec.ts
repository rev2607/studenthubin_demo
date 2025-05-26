import { TestBed } from '@angular/core/testing';

import { StudenthubWebApiService } from './studenthub-web-api.service';

describe('StudenthubWebApiService', () => {
  let service: StudenthubWebApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudenthubWebApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
