import { Component, OnInit } from '@angular/core'; // Ensure OnInit is imported
import { AuthService } from '../services/auth.service'; // Correct import statement
import { Router } from '@angular/router';
import { TruckUser, TruckUsersGetAllResponse } from '../services/api';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderService } from '../services/order.service'; // Make sure this path is correct



@Component({
  selector: 'app-homepage',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'], // Correct the property name to styleUrls
})
export class HomeComponent implements OnInit {
  // Implement OnInit interface
  // You can declare a class property if you need to use it in your template
  userRole: string | null = null;
  CurrentUserisAdmin = false

  currentUserID : string | null = null;
  currentUserTruckId: string | null = null;
  


  constructor(public authService: AuthService, private route: Router, 
    private orderService: OrderService, // Add OrderService here
    private snackBar: MatSnackBar // Add MatSnackBar here if you want to use it
    ) { } // Correctly inject AuthService
  

  ngOnInit(): void {
    // Directly use authService to access getUserRole
    this.userRole = this.authService.getUserRole();


    // If you want to use the token decoding feature to get user details
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = this.authService.decodeJwtToken(token);
      console.log(decodedToken); // Log decoded token details, if necessary
    }


    this.currentUserID = this.authService.getUserId();
if (this.currentUserID) {
    this.orderService.getTruckIdByUserId(this.currentUserID).subscribe({
        next: (data) => {
            this.currentUserTruckId = data.truckId; 
        },
        error: (error) => {
            console.error('Error fetching truck ID:', error);
        }
    });
} else {
    console.error('User ID is null or undefined.');
}
console.log("user id is " + this.currentUserID)
console.log(this.currentUserTruckId)

  }






  location(): void {

    this.route.navigate(['/locationlist'])

  }

  order(): void {
    console.log(this.currentUserTruckId)
    if(!this.currentUserTruckId)
    {
      this.snackBar.open('You are not assigned any truck now ', 'Close', { duration: 3000 });
      return;
    }
    else{
      this.route.navigate(['/displayorder', { currentTruckId: this.currentUserTruckId }])
    }

  }
}
