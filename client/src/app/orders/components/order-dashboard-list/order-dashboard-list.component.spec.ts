import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderDashboardListComponent } from './order-dashboard-list.component';

describe('OrderDashboardListComponent', () => {
  let component: OrderDashboardListComponent;
  let fixture: ComponentFixture<OrderDashboardListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderDashboardListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrderDashboardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
