import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminmonitoringComponent } from './adminmonitoring.component';

describe('AdminmonitoringComponent', () => {
  let component: AdminmonitoringComponent;
  let fixture: ComponentFixture<AdminmonitoringComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminmonitoringComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminmonitoringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
