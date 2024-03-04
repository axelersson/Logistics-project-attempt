import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayorderComponent } from './displayorder.component';

describe('DisplayorderComponent', () => {
  let component: DisplayorderComponent;
  let fixture: ComponentFixture<DisplayorderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayorderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DisplayorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
