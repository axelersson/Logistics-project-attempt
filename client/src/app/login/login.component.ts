import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  onSubmit() {
    // Implement login logic here
    // You can access the entered username and password as this.username and this.password
    // Send a request to your authentication service or backend API for authentication.
  }
}
