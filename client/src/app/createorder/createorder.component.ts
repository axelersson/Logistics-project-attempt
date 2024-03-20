import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderService } from '../services/order.service';
import { Router } from '@angular/router';
import { AuthGuard } from '../security/auth.guard';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-createorder',
  templateUrl: './createorder.component.html',
  styleUrls: ['./createorder.component.css']
})
export class CreateorderComponent implements OnInit {
  order: any = {}; // 初始化订单对象
  orderStatuses = ['Pending', 'PartiallyDelivered', 'Delivered', 'Cancelled']; // 订单状态数组
  orderTypes = ['Recieving', 'Sending']; // 订单类型数组

  constructor(
    private orderService: OrderService,
    private snackBar: MatSnackBar,
    private router: Router,
    private authGuard: AuthGuard,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  createOrder(): void {

    this.order.userId = this.authService.getUserId
    if(this.order.pieces === 0)
    {
      this.snackBar.open('Pieces can not be 0', 'Close', { duration: 3000 });
      return
    }
    if(this.order.orderStatus === 'PartiallyDelivered' && this.order.deliveredPieces === 0)
    {
      this.snackBar.open('DeliveredPieces can not be 0 if the order is partly delieverd', 'Close', { duration: 3000 });
      return
    }
    if(this.order.deliveredPieces > this.order.pieces)
    {
      this.snackBar.open('DeliveredPieces can not be more than pieces', 'Close', { duration: 3000 });
      return
    }
    if((this.order.deliveredPieces === this.order.pieces) && (this.order.orderStatuses != "delivered"))
    {
      this.snackBar.open('If the pieces are all delieverd, the order should be delieverd', 'Close', { duration: 3000 });
      return
    }
    if (this.order.deliveredPieces > this.order.pieces) {
      this.snackBar.open('Delivered pieces cannot be greater than total pieces', 'Close', { duration: 3000 });
      return;
    }
    if (this.order.toLocId === this.order.fromLocId) {
      this.snackBar.open('To Location ID and From Location ID cannot be the same', 'Close', { duration: 3000 });
      return;
    }

    // 调用服务方法创建订单
    this.orderService.newOrder(this.order.userId, this.order).subscribe({
      next: () => {
        this.snackBar.open('Order created successfully', 'Close', { duration: 3000 });
        
      },
      error: (error) => {
        this.snackBar.open('Failed to create the order, please make sure the Order ID is not duplicated and locationId, UserID are valid', 'Close', { duration: 3000 });
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/adminorder']); // 根据你的路由路径调整
  }
}
