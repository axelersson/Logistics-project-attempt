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
  }

  partiallyDeliverOrder(): void {
    if (this.orderId) {
      this.orderService.partialDeliver(this.orderId).subscribe({
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
