import { Component, OnInit } from '@angular/core'; // Ensure OnInit is imported
import { AuthService } from '../services/auth.service'; // Correct import statement
import { Router } from '@angular/router';


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
  constructor(public authService: AuthService, private route: Router) { } // Correctly inject AuthService

  ngOnInit(): void {
    // Directly use authService to access getUserRole
    this.userRole = this.authService.getUserRole();
    console.log(this.userRole); // Log the role to the console

    // If you want to use the token decoding feature to get user details
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = this.authService.decodeJwtToken(token);
      console.log(decodedToken); // Log decoded token details, if necessary
    }
  }

  location(): void {

    this.route.navigate(['/locationlist'])

  }
}
