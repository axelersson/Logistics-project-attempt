import { Component, OnInit } from '@angular/core';
import { DummyDataService } from '../dummy-data.service';
import { Area } from '../services/api';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { AreaDetailsComponent } from './area-detail/area-detail.component'; // Update the import path

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css'],
})
export class AreaComponent implements OnInit {

  areas: Area[] = [];
  areaIdToDelete: string = '';
  selectedArea: Area | undefined;

  constructor(
    private dummyDataService: DummyDataService,
    public dialog: MatDialog
  ) {}
  viewDetails(area: Area): void {
    this.selectedArea = area;
  }

  ngOnInit(): void {
    this.areas = this.dummyDataService.generateDummyAreas(15);
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
        if (result) {
          // If the user confirms, delete the area
          this.areas.splice(index, 1);
          // Optionally, you can make an API call to delete the area on the server
        }
      });
    } else {
      // If the area with the specified ID is not found, show an error message or handle it accordingly
      console.log('Area not found');
    }
    
  }
  
}