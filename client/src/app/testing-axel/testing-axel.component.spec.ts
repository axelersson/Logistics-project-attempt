import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestingAxelComponent } from './testing-axel.component';

describe('TestingAxelComponent', () => {
  let component: TestingAxelComponent;
  let fixture: ComponentFixture<TestingAxelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestingAxelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TestingAxelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
