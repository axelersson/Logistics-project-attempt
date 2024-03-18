import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service'; // Ensure the path is correct
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TruckUser, TruckUsersGetAllResponse } from '../services/api';
import { AuthGuard } from '../security/auth.guard';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-displayorder',
  templateUrl: './displayorder.component.html',
  styleUrls: ['./displayorder.component.css']
})
export class DisplayorderComponent implements OnInit {
  orders: any[] = [];
  assignedOrders = new Set<string>(); // 记录已分配订单的 ID
  truckOrderAssignments: any[] = [];
  assignedTruckUsers: TruckUser[] = [];
  currentUserID = this.authService.getUserId;
  currentUserTruckId: string | null = null;

  

  constructor(
    private orderService: OrderService,
    private snackBar: MatSnackBar,
    private router: Router,
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this.loadOrders();
    this.loadTruckOrderAssignments();
    this.loadAssignedTruckUsers();
    // 获取当前登录用户的 ID
    const currentUserId = this.authService.getUserId();
    // 检查 currentUserId 是否为 null 或 undefined
    if (currentUserId) {
      this.loadAssignedTruckUsersCallback(currentUserId);
    } else {
      // 可能需要处理未登录状态
      console.log('User ID is null, user might not be logged in.');
    }
    console.log("current truck Id:"+this.currentUserTruckId)
    console.log(currentUserId)
  }
  
  loadAssignedTruckUsersCallback(currentUserId: string): void {
    this.orderService.assignedTruckUsers().subscribe({
      next: (data) => {
        this.assignedTruckUsers = data.truckUsers || [];
        // 使用当前用户 ID 查找匹配的 TruckUser
        const userTruckAssignment = this.assignedTruckUsers.find(tu => tu.userId === currentUserId);
        // 检查 userTruckAssignment 是否存在，以及 truckId 是否为 undefined
        if (userTruckAssignment && userTruckAssignment.truckId) {
          this.currentUserTruckId = userTruckAssignment.truckId;
          console.log("Current truck id is"+this.currentUserTruckId)
        } else {
          // 如果找不到匹配的用户或者 truckId 不存在，则设置为 null
          this.currentUserTruckId = null;
        }
      },
      error: (error) => {
        this.snackBar.open('Error getting assigned truck users: ' + error.message, 'Close', {
          duration: 3000,
        });
      }
    });
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

    if (!this.currentUserTruckId) { // 检查当前用户是否有分配的卡车ID
      this.snackBar.open('No truck assigned to the current user', 'Close', {duration: 3000});
      return;
  }

    const truckId = this.currentUserTruckId; // 这里假定你要分配给的卡车 ID

    this.orderService.assignOrder(truckId, orderId).subscribe({
      next: () => {
        this.snackBar.open(`Order ${orderId} assigned successfully`, 'Close', { duration: 3000 });
        this.assignedOrders.add(orderId); // 标记订单已分配
        this.loadTruckOrderAssignments(); // 重新加载分配情况
        this.loadOrders(); // 可选：重新加载订单列表
      },
      error: (error) => {
        this.snackBar.open('Error assigning order: ' + error.message, 'Close', { duration: 3000 });
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

  isOrderAssignmentEstablished(order: any): boolean {
    return this.truckOrderAssignments.some(assignment => assignment.orderId === order.orderId);
  }

  isAssignedToCurrentUser(order: any): boolean {
    return this.truckOrderAssignments.some(assignment => assignment.orderId === order.orderId && assignment.truckId === 'T1-00c40822-2fb0-4449-bd67-31472efc8816');
  }

  isReassignable(order: any): boolean {
    return this.truckOrderAssignments.some(assignment =>
      assignment.orderId === order.orderId && assignment.isAssigned === false);
  }

  isOrderAssigned(order: any): boolean {
    return this.truckOrderAssignments.some(assignment => assignment.isAssigned === true)
  }

  isOrderDeliverd(order: any): boolean {
    return this.truckOrderAssignments.some(assignment => assignment.orderStatus === "Delivered")
  }


  unassignOrder(orderId: string): void {
    const truckId = 'T1-00c40822-2fb0-4449-bd67-31472efc8816'; // 你指定的卡车ID
    this.orderService.unassignTruckFromOrder(orderId, truckId).subscribe({
      next: () => {
        this.snackBar.open(`Order ${orderId} unassigned successfully`, 'Close', { duration: 3000 });
        this.loadTruckOrderAssignments(); // 重新加载分配情况
        this.loadOrders(); // 可选：重新加载订单列表
      },
      error: (error) => {
        this.snackBar.open(`Error unassigning order: ${error.message}`, 'Close', { duration: 3000 });
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

  //find the truckOrderAssignmentAccording to the orderId and change its "IsAssignment" property into true
  assignTruckToOrder(orderId: string): void {

    // 调用OrderService中的assignTruckOrder方法
    this.orderService.assignTruckOrder(orderId).subscribe({
      next: (response) => {
        this.snackBar.open(`Order ${orderId} assigned to truck successfully`, 'Close', { duration: 3000 });
        // 这里可以根据你的需要刷新列表或做其他更新
        this.loadTruckOrderAssignments(); // 重新加载分配情况
        this.loadOrders();
      },
      error: (error) => {
        this.snackBar.open(`Error assigning truck to order: ${error.message}`, 'Close', { duration: 3000 });
      }
    });
  }

//get all users info if these users are assigned with trucks
loadAssignedTruckUsers(): void {
  this.orderService.assignedTruckUsers().subscribe({
    next: (data) => {
      // 检查 data.truckUsers 是否存在，如果不存在则赋值为空数组
      this.assignedTruckUsers = data.truckUsers || [];
    },
    error: (error) => {
      this.snackBar.open('Error getting assigned truck users: ' + error.message, 'Close', {
        duration: 3000,
      });
      // 在发生错误时也应初始化为一个空数组以防止其他错误
      this.assignedTruckUsers = [];
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
