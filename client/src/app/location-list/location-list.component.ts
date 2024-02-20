import { Component, OnInit } from '@angular/core';
import { LocationService } from '../services/location.service';
import { MaterialModule } from '../../Material-Module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrl: './location-list.component.css'
})
export class LocationListComponent {
  locations: any[] = []; // 根据实际数据结构调整类型

  constructor(private locationService: LocationService,private router: Router) {}

  ngOnInit(): void {
    this.locationService.getLocations().subscribe(data => {
      console.log(data)
      this.locations = data; // 根据 API 响应的实际结构调整
    
    });
    this.locations = [
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      {LocationID: 1, Type: "String", AreaID: 1 },
      // 假设这些数据来自后端
    ];
  }
  return() {
    this.router.navigate(['/userlocation'])
    console.log('Operation cancelled.');
  }
}
