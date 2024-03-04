import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'; // Import Location

@Component({
  selector: 'app-view-truck',
  templateUrl: './view-truck.component.html',
  styleUrls: ['./view-truck.component.css']
})
export class ViewTruckComponent implements OnInit {
[x: string]: any;
  truckId: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private location: Location // Inject Location service
  ) {}

  ngOnInit(): void {
    this.truckId = this.route.snapshot.paramMap.get('id');
    // Fetch the truck details using the truckId
  }

  goBack(): void {
    this.location.back(); // Uses the Location service to navigate back
  }
}
