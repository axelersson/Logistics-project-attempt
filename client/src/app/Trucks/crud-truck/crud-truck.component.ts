import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'; // Import Location

@Component({
  selector: 'app-crud-truck',
  templateUrl: './crud-truck.component.html',
  styleUrls: ['./crud-truck.component.css']
})
export class CrudTruckComponent implements OnInit {
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
