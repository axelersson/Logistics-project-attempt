import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedComponentNameComponent } from './shared-component-name.component';

describe('SharedComponentNameComponent', () => {
  let component: SharedComponentNameComponent;
  let fixture: ComponentFixture<SharedComponentNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedComponentNameComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SharedComponentNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
