import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewTruckComponent } from './view-truck.component';

describe('ViewTruckComponent', () => {
  let component: ViewTruckComponent;
  let fixture: ComponentFixture<ViewTruckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewTruckComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewTruckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
