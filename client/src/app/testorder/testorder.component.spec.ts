import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestorderComponent } from './testorder.component';

describe('TestorderComponent', () => {
  let component: TestorderComponent;
  let fixture: ComponentFixture<TestorderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestorderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TestorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
