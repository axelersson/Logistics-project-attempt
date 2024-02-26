import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Client} from '../services/api';


@Component({
  selector: 'app-testorder',
  templateUrl: './testorder.component.html',
  styleUrl: './testorder.component.css'
})
export class TestorderComponent {
  //orders: any[] = []; // Adjust the type according to the actual data structure 

  // *** Creating separate service file ***

  // constructor(private testorderService: TestorderService,private router: Router, private route: ActivatedRoute) {}

  // ngOnInit(): void {

  //   this.testorderService.getTestorders().subscribe(data => {
  //     console.log(data)
  //     this.orders = data; // Adjust to the actual structure of the API response
  //   });    
  // }


  // *** Using Nswag generated Client(Api.js), no need to create separate service file ***
  orders: any[] = [];
  constructor(private client: Client, private router: Router) {}

  ngOnInit(): void {
    // Get all orders on init
    this.client.ordersGET().subscribe(data => {
      console.log(data) // data only contains an object that contains the orders array 
      this.orders = data.orders ?? []; // Thats why we need to access the orders array and assign it to the orders variable, if its null it will be assigned an empty array
      console.log(this.orders) // This should contain the actual orders
    });    
  }

}
