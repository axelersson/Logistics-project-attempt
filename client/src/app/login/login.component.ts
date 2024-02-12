import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Client, LoginRequest } from '../services/api'; // Make sure this is an @Injectable service
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons'; // Import FontAwesome icons

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  // Define the missing properties referenced in the template
  hidePassword: boolean = true;
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  loginError: string | null = null;

  constructor(
    private client: Client, // Ensure this is an @Injectable service
    private router: Router,
    private authService: AuthService,
  ) {}

  onLogin(): void {
    const username = this.loginForm.value.username ?? '';
    const password = this.loginForm.value.password ?? '';
    const loginRequest = new LoginRequest({ username, password });

    console.log('Login request:', loginRequest);

    this.client.login(loginRequest).subscribe(
      (response: any) => {
        console.log('Response:', response);
        // Assuming that the response contains a property named 'token'
        const token = response?.token;

        if (token) {
          console.log('Token:', token);
          // Store the token using the authService
          this.authService.setToken(token);
          // Navigate to the dashboard
          this.router.navigate(['/dashboard']);
        } else {
          console.error('Token not found in response');
          this.loginError = 'Token not found in response';
        }
      },
      (error) => {
        console.error('Login error:', error);
        this.loginError = error.message || 'An error occurred during login.';
      },
    );
  }

  // Method to toggle password visibility
  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}
