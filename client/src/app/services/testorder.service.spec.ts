import { TestBed } from '@angular/core/testing';

import { TestorderService } from './testorder.service';

describe('TestorderService', () => {
  let service: TestorderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestorderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
