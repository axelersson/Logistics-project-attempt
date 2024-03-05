import { Component, OnInit } from '@angular/core';
import { LocationService } from '../services/location.service';
import { MaterialModule } from '../../Material-Module';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrl: './location-list.component.css'
})
export class LocationListComponent {
  locations: any[] = []; // 根据实际数据结构调整类型
  backpage = ''
  colorMap = new Map();

  colors = ['#00EE76', '#6495ED']; // 示例颜色
  areaColorMap = new Map();

  constructor(private locationService: LocationService,private router: Router, private route: ActivatedRoute, 
    private authservice: AuthService, private matSnackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.backpage = params['backpage'];
    });

    this.locationService.getLocations().subscribe(data => {
      console.log(data)
      this.locations = data; // 根据 API 响应的实际结构调整
      this.transform(this.locations)
      this.assignColors();
    });
  }

  return() {
      this.router.navigate(['/homepage'])
  }
  
  next(){
    if(this.authservice.isAdmin())
      {
        this.router.navigate(['/adminlocation'])
      }
    else
     {
      this.matSnackBar.open('This operation does not support regular users ', 'Close', { duration: 3000 });
     }  
  }

  assignColors(): void {
    let currentAreaId = '';
    let colorIndex = 0; // 用于从颜色数组中选择颜色

    this.locations.forEach(location => {
      if (location.areaId !== currentAreaId) {
        currentAreaId = location.areaId;
        if (colorIndex >= this.colors.length) colorIndex = 0; // 如果颜色用完了，重新开始
        this.areaColorMap.set(currentAreaId, this.colors[colorIndex++]);
      }
      location.color = this.areaColorMap.get(currentAreaId); // 将颜色分配给location
    });
  }

  transform(locations: any[]): any[] {
    if (!locations) return [];
    return locations.sort((a, b) => a.areaId.localeCompare(b.areaId)); //sortedByString
  }

}
