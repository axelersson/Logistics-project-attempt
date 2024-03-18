import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { OrderService } from '../services/order.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-completeorder',
  templateUrl: './completeorder.component.html',
  styleUrl: './completeorder.component.css'
})
export class CompleteorderComponent implements OnInit{
  orderId: string | null = null;
  order: any = {}; // 根据你的需求定义更准确的类型
  delieverdPieces = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.orderId = params['orderId'];
    });

    if (this.orderId) {
      this.orderService.getOrderById(this.orderId).subscribe({
        next: (data) => {
          this.order = data; // 填充表单数据
          console.log(data);
          this.delieverdPieces = this.order.deliveredPieces;
        },
        error: (error) => {
          this.snackBar.open(`Error retrieving order: ${error}`, 'Close', { duration: 3000 });
          this.router.navigate(['/adminorder']); // 如果出错，返回订单列表页面
        }
      });
    }

  }

  partiallyDeliverOrder(): void {
    if(this.delieverdPieces === 0)
    {
      this.snackBar.open('Delieverd pieces can not be zero!', 'Close', { duration: 3000 });
      return
    }

    if(this.delieverdPieces>= this.order.pieces)
    {
      this.snackBar.open('Delieverd pieces can not be greater than pieces or equal to pieces!', 'Close', { duration: 3000 });
      return
    }

    if (this.orderId) {
      this.orderService.partialDeliver(this.orderId,this.delieverdPieces).subscribe({
        next: () => {
          this.snackBar.open('Order partially delivered', 'Close', { duration: 3000 });
          this.router.navigate(['/adminorder']);
        },
        error: (error) => this.snackBar.open(error.message, 'Close', { duration: 3000 })
      });
    }
  }

  completelyDeliverOrder(): void {
    if (this.orderId) {
      this.orderService.deliver(this.orderId).subscribe({
        next: () => {
          this.snackBar.open('Order completely delivered', 'Close', { duration: 3000 });
          this.router.navigate(['/adminorder']);
        },
        error: (error) => this.snackBar.open(error.message, 'Close', { duration: 3000 })
      });
    }
  }
}
