import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client } from '../../services/api';
import { Area } from '../../services/api';

@Component({
  selector: 'app-area-detail',
  templateUrl: './area-detail.component.html',
  styleUrls: ['./area-detail.component.css'],
})
export class AreaDetailsComponent implements OnInit {
  @Input() area: Area | undefined;
  locations: any[] = [];
  
  constructor(
    private route: ActivatedRoute,
    private client: Client
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const areaId = params.get('areaId');
      if (areaId) {
        // Fetch area details using areaId from the Client service
        this.client.areasGET2(areaId).subscribe(
          (area: Area) => {
            this.area = area;
            this.locations = area.locations ?? [];
            console.log("Locations: ", this.locations)
          },
          (error) => {
            console.error('Error fetching area details:', error);
          }
        );
      }
    });
  }
}