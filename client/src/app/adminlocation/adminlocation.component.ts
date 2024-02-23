import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocationService } from '../services/location.service';

@Component({
  selector: 'app-adminlocation',
  templateUrl: './adminlocation.component.html',
  styleUrl: './adminlocation.component.css'
})
export class AdminlocationComponent {
  locations: any[] = [];

  constructor(private router: Router, private locationService: LocationService) { }

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

  createView() {
    // 实现查看逻辑
    console.log('Viewing location...');
  }

  deleteLocation() {
    // 实现删除逻辑
    console.log('Deleting location...');
  }

  createLocation() {
    // 实现创建逻辑
    this.router.navigate(['/adminedit'])
    console.log('Creating new location...');
  }

  editLocation() {
    // 实现编辑逻辑
    this.router.navigate(['/adminedit'])
    console.log('Editing location...');
  }

  cancel() {
    // 实现取消逻辑
    console.log('Operation cancelled.');
  }

}
