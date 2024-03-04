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

  constructor(
    private orderService: OrderService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadOrders();
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

  returnBack(): void {
    this.router.navigate(['/adminorder']); // Adjust this if you have a specific path to return to
  }
}
