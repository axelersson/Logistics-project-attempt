import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocationService } from '../services/location.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-adminlocation',
  templateUrl: './adminlocation.component.html',
  styleUrl: './adminlocation.component.css'
})
export class AdminlocationComponent {
  locations: any[] = [];
  selectedLocationId: string | null = null;
  backpage = 'adminlocation'

  constructor(private router: Router, private locationService: LocationService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.locationService.getLocations().subscribe({
      next: (data) => {
        this.locations = data;
        console.log(this.locations);
      },
      error: (err) => {
        console.error('Error fetching locations:', err);
      }
    });
    
  }

  view() {
    // 实现查看逻辑
    console.log('Viewing location...');
    this.router.navigate(['locationlist'],{ queryParams: { backpage: this.backpage } })
  }

  deleteSelectedLocation(): void {
    if (this.selectedLocationId) {
      this.locationService.deleteLocation(this.selectedLocationId).subscribe({
          next: () => {
              this.snackBar.open('Location deleted successfully', 'Close', { duration: 3000 });
              this.locations = this.locations.filter(location => location.locationId !== this.selectedLocationId);
              this.selectedLocationId = null; // 清空选中的 locationId
          },
          error: (err) => {
              console.error('Error deleting location:', err);
              this.snackBar.open('Failed to delete location', 'Close', { duration: 3000 });
          }
      });
  } else {
      this.snackBar.open('No location selected ', 'Close', { duration: 3000 });

  }
}


  createLocation() {
    // 实现创建逻辑
    this.router.navigate(['/adminedit'])
    console.log('Creating new location...');
  }

  editLocation() {
    if (!this.selectedLocationId) {
      // 如果没有选中任何位置，则显示提示消息
      this.snackBar.open('Please select a location to edit', 'Close', { duration: 3000 });
    } else {
      // 如果选中了位置，则导航到编辑页面，并传递选中的 locationId
    this.router.navigate(['/adminedit', { locationId: this.selectedLocationId }])
    console.log('Editing location...');
    }
   
  }

  cancel() {
    // 实现取消逻辑
    this.router.navigate(['/homepage'])
    console.log('Operation cancelled.');
  }

}
