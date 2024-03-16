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
    // this.getTruckOrdersAssignmentsIfAssigned();
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
    const truckId = 'T1-00c40822-2fb0-4449-bd67-31472efc8816'; // 这里假定你要分配给的卡车 ID
    this.orderService.assignOrder(truckId, orderId).subscribe({
      next: () => {
        this.snackBar.open(`Order ${orderId} assigned successfully`, 'Close', {duration: 3000});
        this.assignedOrders.add(orderId); // 标记订单已分配
        this.loadOrders(); // 可选：重新加载订单列表
        window.location.reload();
      },
      error: (error) => {
        this.snackBar.open('Error assigning order: ' + error.message, 'Close', {duration: 3000});
      }
    });
    
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
  return this.truckOrderAssignments.some(assignment => assignment.orderId === order.orderId && assignment.truckId === 'T1-00c40822-2fb0-4449-bd67-31472efc8816');
}

isReassignable(order: any): boolean {
  return this.truckOrderAssignments.some(assignment => 
    assignment.orderId === order.orderId && assignment.isAssigned === false);
}

unassignOrder(orderId: string): void {
  const truckId = 'T1-00c40822-2fb0-4449-bd67-31472efc8816'; // 你指定的卡车ID
  this.orderService.unassignTruckFromOrder(orderId, truckId).subscribe({
      next: () => {
          this.snackBar.open(`Order ${orderId} unassigned successfully`, 'Close', {duration: 3000});
          this.loadTruckOrderAssignments(); // 重新加载分配情况
          this.loadOrders(); // 可选：重新加载订单列表
      },
      error: (error) => {
          this.snackBar.open(`Error unassigning order: ${error.message}`, 'Close', {duration: 3000});
      }
  });
}

// 新方法：获取当前卡车用户的所有订单分配情况
getTruckOrdersAssignmentsIfAssigned(): void {
  const truckId = 'T1-00c40822-2fb0-4449-bd67-31472efc8816'; // 替换为实际的卡车ID
  this.orderService.getTruckOrdersAssignmentIfisAssignedEqualsTrue(truckId).subscribe({
      next: (assignments) => {
          // 检查 assignments.truckOrderAssignments 是否为 undefined
          this.truckOrderAssignments = assignments.truckOrderAssignments ? assignments.truckOrderAssignments : [];
      },
      error: (error) => {
          this.snackBar.open(`Error getting truck orders: ${error.message}`, 'Close', { duration: 3000 });
          // 发生错误时也应初始化为一个空数组以防止其他错误
          this.truckOrderAssignments = [];
      }
  });
}


  return(): void {
    this.router.navigate(['/homepage']); // Adjust this if you have a specific path to return to
  }
  next(): void {
    this.router.navigate(['/adminorder']); // Adjust this if you have a specific path to return to
  }
}
