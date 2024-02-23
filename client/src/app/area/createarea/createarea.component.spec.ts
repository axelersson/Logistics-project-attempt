import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateareaComponent } from './createarea.component';

describe('CreateareaComponent', () => {
  let component: CreateareaComponent;
  let fixture: ComponentFixture<CreateareaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateareaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
