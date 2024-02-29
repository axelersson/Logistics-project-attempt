import { ComponentFixture, TestBed } from '@angular/core/testing';

import {CrudTruckComponent } from './crud-truck.component';

describe('ViewTruckComponent', () => {
  let component: CrudTruckComponent;
  let fixture: ComponentFixture<CrudTruckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrudTruckComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CrudTruckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
