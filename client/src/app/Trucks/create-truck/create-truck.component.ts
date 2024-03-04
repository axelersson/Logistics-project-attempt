import { Component } from '@angular/core';
import { Client, Truck } from '../../services/api'; // Adjust the import path as necessary
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-create-truck',
  templateUrl: './create-truck.component.html',
  styleUrls: ['./create-truck.component.css'] // Note the correction from styleUrl to styleUrls
})
export class CreateTruckComponent {
  truck: Truck;

  constructor(private client: Client, private router: Router, private snackBar: MatSnackBar) {
    this.truck = new Truck();
    // Initialize your truck properties here
    this.truck.truckId = '';
    // this.truck.registrationnumber = ''; // Assuming your Truck model has a registrationNumber
    // Add other necessary initializations
  }

  confirm(): void {
    // Validation logic
    if (this.truck.truckId == '' || this.truck.currentAreaId == '' || this.truck.registrationnumber == '') {
      this.snackBar.open('Please complete all fields', 'Close', { duration: 5000 });
      return;
    } else {
      // API call to create the truck
      this.client.trucksPOST(this.truck).subscribe(
        (response: Truck) => {
          console.log('Truck created successfully', response);
          this.snackBar.open('Truck created successfully', 'Close', { duration: 5000 });
          // Optionally redirect the user or reset the form
        },
        (error) => {
          console.error('Error creating truck:', error);
          this.snackBar.open('Error creating truck', 'Close', { duration: 5000 });
        }
      );
    }
  }
  updateTruck(): void {
    console.log(this.truck);
    if (this.truck.truckId == '' || this.truck.currentAreaId == '' || this.truck.registrationnumber == ''){
      const snackBarRef = this.snackBar.open('Please complete all fields', 'Close', { duration: 1500 });
    setTimeout(() => {
      snackBarRef.dismiss();
    }, 500);
    this.client.trucksPUT(this.truck.truckId ?? '', this.truck).subscribe(
      (response) => {
        console.log('Truck updated successfully', response);
        this.snackBar.open('Truck updated successfully', 'Close', { duration: 1500 });
      },
      (error: any) => {
        console.error('Error updating Truck:', error);
        this.snackBar.open('Could not update Truck, check that the truck ID is correct.', 'Close', { duration: 1500 });
      }
    );
  }
}

  // Any additional methods for CreateTruckComponent
}


