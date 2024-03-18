import { Component, OnInit } from '@angular/core';
import { AreaService } from '../services/area.service';
import { Client } from '../services/api'
import { Area } from '../services/api';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service'

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css'],
})
export class AreaComponent implements OnInit {
  areas: any[] = [];
  areaIdToDelete: string = '';
  selectedArea: Area | undefined;

  constructor(private client: Client, private router: Router, public dialog: MatDialog, public authService: AuthService) { }

  viewDetails(area: Area): void {
    this.selectedArea = area;
  }

  ngOnInit(): void {
    this.client.areasGET().subscribe(data => {
      console.log(data)
      this.areas = data.areas ?? [];
      console.log(this.areas)
    })
    // this.areaService.areas$.subscribe((areas) => {
    //   this.areas = areas;
    // });

    // Fetch areas when the component initializes
    // this.areaService.fetchAllAreas();
  }

  deleteArea(): void {
    // Find the index of the area with the specified ID
    const index = this.areas.findIndex((area) => area.areaId === this.areaIdToDelete);

    // If the area is found, show a confirmation dialog
    if (index !== -1) {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        data: { message: 'Are you sure you want to delete this area?' },
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