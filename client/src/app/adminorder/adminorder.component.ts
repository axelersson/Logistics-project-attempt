import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderService } from '../services/order.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-adminorder',
  templateUrl: './adminorder.component.html',
  styleUrl: './adminorder.component.css'
})
export class AdminorderComponent implements OnInit{
  orders: any[] = [];
  selectedOrderId: string | null = null;
  currentUserTruckId: string | null = null;
  truckOrderAssignments: any[] = [];
  currentUserRole: string | null = null;


  constructor(private orderService: OrderService,private router: Router,
    private snackBar: MatSnackBar,public authservice: AuthService,private route: ActivatedRoute) {}

  ngOnInit(): void {

    const truckId = this.route.snapshot.paramMap.get('currentTruckId');
    this.currentUserTruckId = truckId;
    // console.log(truckId);
    // console.log(this.currentUserTruckId);

    this.getCurrentUserRole();
    this.loadTruckOrderAssignments();
    this.loadOrders();
  }

  getCurrentUserRole(): void {
    // 假设 AuthService 有一个方法返回当前用户的角色
    this.currentUserRole = this.authservice.getUserRole();
  }

  loadOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (data) => {
        
        this.orders = data; // 假设后端直接返回订单数组
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  displayOrderFilter(): any[] {
    // Filter orders based on the conditions given
    return this.orders.filter(order => 
      this.truckOrderAssignments.some(assignment => 
        assignment.orderId === order.orderId && 
        assignment.truckId === this.currentUserTruckId && 
        assignment.isAssigned === true
      )
    );
  }

  loadTruckOrderAssignments(): void {
    this.orderService.getTruckOrderAssignments().subscribe({
      next: (data) => {
        this.truckOrderAssignments = data; // Assuming the backend directly returns an array of truck order assignments
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  completeTheOrder():void{
    if (this.selectedOrderId){
      this.router.navigate(['/completeorder'], { queryParams: { orderId: this.selectedOrderId } });
    }
    else{
      this.snackBar.open('No order selected ', 'Close', { duration: 3000 });
    }
  }

  createOrder():void{
    this.router.navigate(['createorder'])
  }

  displayOrder():void{
    this.router.navigate(['displayorder'])
  }


cancelSelectedOrder(): void {
  if (this.selectedOrderId) {
    this.orderService.cancelOrder(this.selectedOrderId).subscribe({
      next: () => {
        this.snackBar.open('Order cancelled successfully', 'Close', { duration: 3000 });
        this.loadOrders(); // 重新加载订单，以更新界面
      },
      error: (error) => {
        this.snackBar.open(error.message, 'Close', { duration: 3000 });
      }
    });
  } else {
    this.snackBar.open('Please select an order to cancel', 'Close', { duration: 3000 });
  }
}

cancel():void{
  this.router.navigate(['/displayorder'])
}

// 在 AdminorderComponent 中
updateSelectedOrder(): void {
  if (this.selectedOrderId) {
    this.router.navigate(['/updateorder'], { queryParams: { orderId: this.selectedOrderId } });
  } else {
    this.snackBar.open('Please select an order to update', 'Close', { duration: 3000 });
  }
}

}
