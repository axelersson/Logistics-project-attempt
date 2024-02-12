import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TryHomepageComponent } from './try-homepage.component';

describe('TryHomepageComponent', () => {
  let component: TryHomepageComponent;
  let fixture: ComponentFixture<TryHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TryHomepageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TryHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
