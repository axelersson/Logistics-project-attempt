import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service'; // Ensure the path is correct
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-displayorder',
  templateUrl: './displayorder.component.html',
  styleUrls: ['./displayorder.component.css']
})
export class DisplayorderComponent implements OnInit {
  orders: any[] = [];
  assignedOrders = new Set<string>(); // 记录已分配订单的 ID
  truckOrderAssignments: any[] = [];

  constructor(
    private orderService: OrderService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadOrders();
    this.loadTruckOrderAssignments();
  }

  loadOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (data) => {
        this.orders = data;
      },
      error: (err) => {
        this.snackBar.open('Error fetching orders: ' + err.message, 'Close', {
          duration: 3000,
        });
      },
    });
  }

  assignOrder(orderId: string): void {
    const truckId = 'T1-f12127d2-99a2-4385-8777-6f3415e4e470'; // 这里假定你要分配给的卡车 ID
    this.orderService.assignOrder(truckId, orderId).subscribe({
      next: () => {
        this.snackBar.open(`Order ${orderId} assigned successfully`, 'Close', {duration: 3000});
        this.assignedOrders.add(orderId); // 标记订单已分配
        this.loadOrders(); // 可选：重新加载订单列表
      },
      error: (error) => {
        this.snackBar.open('Error assigning order: ' + error.message, 'Close', {duration: 3000});
      }
    });
    this.loadTruckOrderAssignments();
  }

  loadTruckOrderAssignments(): void {
    this.orderService.getTruckOrderAssignments().subscribe({
        next: (assignments) => {
            this.truckOrderAssignments = assignments;
        },
        error: (error) => {
            this.snackBar.open(`Error fetching truck order assignments: ${error.message}`, 'Close', {
                duration: 3000,
            });
        }
    });
}

isOrderAssigned(order: any): boolean {
  return this.truckOrderAssignments.some(assignment => assignment.orderId === order.orderId);
}

isAssignedToCurrentUser(order: any): boolean {
  return this.truckOrderAssignments.some(assignment => assignment.orderId === order.orderId && assignment.truckId === 'T3');
}


  return(): void {
    this.router.navigate(['/homepage']); // Adjust this if you have a specific path to return to
  }
  next(): void {
    this.router.navigate(['/adminorder']); // Adjust this if you have a specific path to return to
  }
}
