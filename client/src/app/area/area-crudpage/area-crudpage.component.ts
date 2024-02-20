import { Component, OnInit, Input } from '@angular/core';
import { DummyDataService } from '../../dummy-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Area } from '../../services/api';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-area-crudpage',
  templateUrl: './area-crudpage.component.html',
  styleUrls: ['./area-crudpage.component.css']
})
export class AreaCrudpageComponent implements OnInit {
  areas: Area[] = [];
  areaIdToDelete: string = '';
  @Input() area: Area | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dummyDataService: DummyDataService,
    public dialog: MatDialog
  ) {}

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

  viewArea(): void {
    // Redirect to the area details page with the specified ID
    this.router.navigate(['/area', this.areaIdToDelete]);
  }
}