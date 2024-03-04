import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Client } from '../../services/api'
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-area-crudpage',
  templateUrl: './area-crudpage.component.html',
  styleUrls: ['./area-crudpage.component.css']
})
export class AreaCrudpageComponent implements OnInit {
  areas: any[] = [];
  areaIdToDelete: string = '';
  selectedAreaId: string | null = null;
  @Input() area: any | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog,
    private client: Client
  ) {}

  ngOnInit(): void {
    this.client.areasGET().subscribe(data => {
      console.log(data)
      this.areas = data.areas ?? [];
      console.log(this.areas)
    })
  }

  deleteArea(): void {
    const areaIdToDelete = this.selectedAreaId ?? ''
    // Find the index of the area with the specified ID
    //const index = this.areas.findIndex((area) => area.areaId === this.areaIdToDelete);
  
    console.log("Area to delete:", areaIdToDelete)
    console.log(typeof areaIdToDelete)
    // If the area is found, show a confirmation dialog
    if (true) {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        data: { message: 'Are you sure you want to delete this area?' },
      }); 

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          // If the user confirms, delete the area
          this.client.areasDELETE(areaIdToDelete).subscribe(
            () => {
              //this.areas.splice(index, 1);
              console.log('Area deleted successfully');
            },
            (error) => {
              console.error('Error deleting area:', error);
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

  viewArea(): void {
    // Redirect to the area details page with the specified ID
    this.router.navigate(['/area', this.selectedAreaId]);
  }
}