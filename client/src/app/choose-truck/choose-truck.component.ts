import { Component, OnInit } from '@angular/core';
//import { TrucksService } from '../services/trucks.service'; // Adjust path as necessary
import { Truck } from '../services/api';
//import { Truck } from '../truck.model'; // Assuming you have a Truck model
import { ITruck } from '../services/api';

@Component({
  selector: 'app-choose-truck',
  templateUrl: './choose-truck.component.html',
  styleUrls: ['./choose-truck.component.css']
})
export class TruckListComponent implements OnInit {
  trucks: ITruck[] = [
    {
      truckId: 'T001',
      currentAreaId: 'A001',
      // Assuming the details for truckUsers and truckOrderAssignments are not needed for the current display
      truckUsers: undefined,
      truckOrderAssignments: undefined,
      //status: 'Active' // Or any other value that fits the 'status' property's type
    },
    {
      truckId: 'T002',
      currentAreaId: 'A002',
      truckUsers: undefined,
      truckOrderAssignments: undefined,
      //status: 'In Transit'
    },
    // Add more mock trucks as needed
  ];

  constructor() { }

  ngOnInit(): void {
  }

}