import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocationService } from '../services/location.service';

@Component({
  selector: 'app-userlocation',
  templateUrl: './userlocation.component.html',
  styleUrl: './userlocation.component.css'
})
export class UserlocationComponent {
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
    this.router.navigate(['/locationlist'])
    console.log('Viewing location...');
  }

  cancel() {
    // 实现取消逻辑
    console.log('Operation cancelled.');
  }

}
