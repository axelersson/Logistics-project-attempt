import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-truck-edit',
  templateUrl: './truck-edit.component.html',
  styleUrls: ['./truck-edit.component.css']
})
export class TruckEditComponent {
  truckId: string | undefined;
  areaId: string | undefined;

  constructor(private router: Router) {}

  cancel() {
    this.router.navigate(['/truck-admin']); // Adjust the route as necessary
  }

  confirm() {
    // Implement the logic to confirm the changes
    console.log('Confirming changes for Truck ID:', this.truckId, 'Area ID:', this.areaId);
    // Potentially navigate away after confirming the changes
  }
}
