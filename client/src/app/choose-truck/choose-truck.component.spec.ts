import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseTruckComponent } from './choose-truck.component';

describe('ChooseTruckComponent', () => {
  let component: ChooseTruckComponent;
  let fixture: ComponentFixture<ChooseTruckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChooseTruckComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChooseTruckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});