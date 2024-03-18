import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrucklistComponent } from './trucklist.component';

describe('TrucklistComponent', () => {
  let component: TrucklistComponent;
  let fixture: ComponentFixture<TrucklistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TrucklistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrucklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
