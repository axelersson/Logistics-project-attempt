import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Location } from '@angular/common';
import { Client, TruckUser} from "../services/api"

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent implements OnInit {
  isLoggedIn: boolean = true;
  isAdmin: boolean = false;
  trucks: any[] = [];
  yourTruckId: string | null = null;
  userId: string | null = null;

  constructor(
    private authService: AuthService,
    private location: Location,
    private client: Client
  ) {}

  ngOnInit(): void {

    this.authService.isLoggedInObservable.subscribe((status) => {
      this.isLoggedIn = status;
      // Here you can also set isAdmin based on the decoded token if needed
    });
    this.client.trucksGET().subscribe(data => {
      console.log(data)
      this.trucks = data.trucks ?? [];
      console.log("NAVTRUCKS", this.trucks)
      this.trucks.forEach(truck => {
        console.log("truck Users NAV", truck.truckUsers)
        console.log("truck Users NAV", truck.truckUsers.length)
        truck.truckUsers.forEach((truckUser: { userId: string | null; truckId: string | null; }) => {
          if (truckUser.userId === this.userId){
            this.yourTruckId = truckUser.truckId;
            console.log("Truck in nav: ", this.yourTruckId)
          }
        })
      }) // This is where the closing bracket for the forEach loop should be
      console.log("YOUR NAVTRUCK", this.yourTruckId)
    });
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = this.authService.decodeJwtToken(token);
      console.log(decodedToken); // Log decoded token details, if necessary
    }
    this.userId = this.authService.getUserId();
    console.log("NAV USERID", this.userId) 

  }

  logout(): void {
    this.authService.logout();
  }

  goBack(): void {
    this.location.back();
  }
}
