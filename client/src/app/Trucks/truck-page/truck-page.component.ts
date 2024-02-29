import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Client, Truck } from '../../services/api'
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

@Component({
   selector: 'app-truck-page',
   templateUrl: './truck-page.component.html',
   styleUrls: ['./truck-page.component.css']
 })
export class TruckPageComponent implements OnInit {
  trucks: any[] = [];
  selectedTruckId: Truck | undefined;
  truckIdToDelete: string = '';
  @Input() truck: any | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog,
    private client: Client,
  ) {}

  ngOnInit(): void {
    this.client.trucksGET().subscribe(data => {
       console.log(data)
       this.trucks = data.trucks ?? [];
       console.log(this.trucks)
    })
  }

  deleteTruck(): void {
    const truckIdToDelete = this.selectedTruckId?.truckId ?? '';

    // Find the index of the area with the specified ID
    //const index = this.areas.findIndex((area) => area.areaId === this.areaIdToDelete);

    // If the area is found, show a confirmation dialog
    if (true) {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        data: { message: 'Are you sure you want to delete this truck?' },
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          // If the user confirms, delete the area
          this.client.trucksDELETE(truckIdToDelete).subscribe(
            () => {
              //this.areas.splice(index, 1);
              console.log('Truck deleted successfully');
            },
            (error) => {
              console.error('Error deleting Truck:', error);
              // Handle error accordingly
            }
          );
        }
      });
    }  else {
      // If the area with the specified ID is not found, show an error message or handle it accordingly
      console.log('Area not found');
    }
  }

  viewTruck(): void {
    // Redirect to the area details page with the specified ID
    this.router.navigate(['/truckpage', this.selectedTruckId]);
  }
}

// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { Client } from '../../services/api'; // Adjust the import path as necessary
// import { Truck } from '../../services/api'; // Adjust the import path as necessary

// @Component({
//   selector: 'app-truck-page',
//   templateUrl: './truck-page.component.html',
//   styleUrls: ['./truck-page.component.css']
// })

// export class TruckPageComponent implements OnInit {
//   trucks: any[] = [];
//   selectedTruck: Truck | undefined;

//   constructor(private client: Client, private router: Router){ }

//   ngOnInit(): void {
//     this.client.trucksGET().subscribe(data => {
//       console.log(data)
//       this.trucks = data.trucks ?? [];
//       console.log(this.trucks)
//     })
//   }
// }
