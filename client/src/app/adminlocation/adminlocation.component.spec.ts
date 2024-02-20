import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminlocationComponent } from './adminlocation.component';

describe('AdminlocationComponent', () => {
  let component: AdminlocationComponent;
  let fixture: ComponentFixture<AdminlocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminlocationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminlocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
