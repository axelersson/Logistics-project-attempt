import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-updateorder',
  templateUrl: './updateorder.component.html',
  styleUrl: './updateorder.component.css'
})
export class UpdateorderComponent {
  orderStatuses: string[] = ['Pending', 'PartiallyDelivered', 'Delivered', 'Cancelled'];
  orderId: string | null = null;
  order: any = {}; // 根据你的需求定义更准确的类型

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => { // 注意这里改成了 queryParams
      this.orderId = params['orderId']; // 获取查询参数
      if (this.orderId) {
        this.orderService.getOrderById(this.orderId).subscribe({
          next: (data) => {
            this.order = data; // 填充表单数据
            console.log(data);
          },
          error: (error) => {
            this.snackBar.open(`Error retrieving order: ${error}`, 'Close', { duration: 3000 });
            this.router.navigate(['/adminorder']); // 如果出错，返回订单列表页面
          }
        });
      }
    });
  }


  updateOrder(): void {
    console.log(this.orderId)
    console.log(this.order)
    if(this.order.pieces === 0)
    {
      this.snackBar.open('Pieces can not be 0', 'Close', { duration: 3000 });
      return
    }
    if(this.order.toLocId === this.order.fromLocId)
    {
      this.snackBar.open('Fromloc and Toloca can not be the same', 'Close', { duration: 3000 });
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
    if((this.order.deliveredPieces === this.order.pieces) && (this.order.orderStatuses != "Delivered"))
    {
      this.snackBar.open('If the pieces are all delieverd, the order should be delieverd', 'Close', { duration: 3000 });
      return
    }
    

    if (this.orderId && this.order ) {
      this.orderService.updateOrder(this.orderId, this.order).subscribe({
        next: () => {
          this.snackBar.open('Order updated successfully', 'Close', { duration: 3000 });
          this.router.navigate(['/adminorder']);
        },
        error: (error) => this.snackBar.open("Please use valid ID and do not use duplicated locationId", 'Close', { duration: 3000 })
      });
    } else {
      this.snackBar.open('Please fill all required fields', 'Close', { duration: 3000 });
    }
  }

  cancel(): void {
    this.router.navigate(['/adminorder']);
  }
}
