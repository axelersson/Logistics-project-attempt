import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TruckAdminComponent } from './truck-admin.component';

describe('TruckAdminComponent', () => {
  let component: TruckAdminComponent;
  let fixture: ComponentFixture<TruckAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TruckAdminComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TruckAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
