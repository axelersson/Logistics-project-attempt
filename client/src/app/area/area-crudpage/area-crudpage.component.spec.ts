import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AreaCrudpageComponent } from './area-crudpage.component';

describe('AreaCrudpageComponent', () => {
  let component: AreaCrudpageComponent;
  let fixture: ComponentFixture<AreaCrudpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AreaCrudpageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AreaCrudpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
