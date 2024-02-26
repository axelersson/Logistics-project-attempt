import { Component, OnInit } from '@angular/core';
import { LocationService } from '../services/location.service';
import { MaterialModule } from '../../Material-Module';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrl: './location-list.component.css'
})
export class LocationListComponent {
  locations: any[] = []; // 根据实际数据结构调整类型 
  backpage = ''

  constructor(private locationService: LocationService,private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.backpage = params['backpage'];
    });

    this.locationService.getLocations().subscribe(data => {
      console.log(data)
      this.locations = data; // 根据 API 响应的实际结构调整
    
    });
    
  }
  return() {
    if(this.backpage == 'adminlocation')
    {
      this.router.navigate(['/adminlocation'])
    }
    else
    {
      this.router.navigate(['/userlocation'])
    }
  }
}
