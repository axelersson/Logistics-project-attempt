import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TruckPageComponent } from './truck-page.component';

describe('TruckPageComponent', () => {
  let component: TruckPageComponent;
  let fixture: ComponentFixture<TruckPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TruckPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TruckPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});