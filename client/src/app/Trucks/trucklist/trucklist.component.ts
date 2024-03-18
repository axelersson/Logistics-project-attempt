import { Component, OnInit } from '@angular/core';
import { Client, TruckUser } from '../../services/api'
import { Truck } from '../../services/api';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-trucklist',
  templateUrl: './trucklist.component.html',
  styleUrls: ['./trucklist.component.css'],
})
export class TrucklistComponent implements OnInit {
  trucks: any[] = [];
  userRole: string | null = null;
  userId: string | null = null;
  CurrentUserisAdmin = false;
  truckIdToDelete: string = '';
  selectedTruck: Truck | undefined;


  constructor(public authService: AuthService, private client: Client, private router: Router, public dialog: MatDialog) { }

  viewDetails(truck: Truck): void {
    this.selectedTruck = truck;
  }

  ngOnInit(): void {
    this.client.trucksGET().subscribe(data => {
      console.log(data)
      this.trucks = data.trucks ?? [];
      console.log(this.trucks)
    })
    this.userId = this.authService.getUserId();
    console.log(this.userId)
    this.userRole = this.authService.getUserRole();
    console.log(this.userRole); // Log the role to the console

    // If you want to use the token decoding feature to get user details
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = this.authService.decodeJwtToken(token);
      console.log(decodedToken); // Log decoded token details, if necessary
    }
  }

  assignUser(truck: Truck): void {
    let truckId = '';
    if (truck.truckId != null) {
      truckId = truck.truckId;
    }
    let userId = '';
    if (this.userId != null) {
      userId = this.userId;
    }
    console.log(userId)
    console.log(truckId)
    if (truckId !== '' && userId !== '') {
      this.client.assignUser(truckId, userId).subscribe(
        () => {
          // const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
          //   data: { message: 'Do you want to assign this truck to yourself?' },
          // });
          console.log('truck assigned to user');
        }, (error) => {
          console.log(error)
        }
      );

      console.log(truck.truckId); // Print the truck ID
    } else {
      console.log("Något gick fel")
    }
  }

  deleteArea(): void {
    // Find the index of the area with the specified ID
    const index = this.trucks.findIndex((truck) => truck.truckId === this.truckIdToDelete);

    // If the area is found, show a confirmation dialog
    if (index !== -1) {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        data: { message: 'Are you sure you want to delete this truck?' },
      });

      dialogRef.afterClosed().subscribe((result) => {
        // if (result) {
        //   // If the user confirms, delete the area
        //   this.areaService.deleteArea(this.areaIdToDelete).subscribe(
        //     () => {
        //       // Update the areas array after successful deletion
        //       this.areas.splice(index, 1);
        //     },
        //     (error) => {
        //       // Handle error, you might want to show an error message to the user
        //       console.error('Error deleting area:', error);
        //     }
        //   );
        // }
      });
    } else {
      // If the area with the specified ID is not found, show an error message or handle it accordingly
      console.log('Area not found');
    }
  }
}