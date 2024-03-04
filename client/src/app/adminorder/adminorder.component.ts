import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderService } from '../services/order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminorder',
  templateUrl: './adminorder.component.html',
  styleUrl: './adminorder.component.css'
})
export class AdminorderComponent implements OnInit{
  orders: any[] = [];
  selectedOrderId: string | null = null;

  constructor(private orderService: OrderService,private router: Router,private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (data) => {
        console.log('Received data:', data);
        this.orders = data; // 假设后端直接返回订单数组
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
}
