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

    // Make sure that the login method returns a Promise and the response has a token property
    this.client.login(loginRequest).subscribe((response: any) => {
      // Replace 'any' with the actual response type
      const token = response.token;

      if (token) {
        const decodedToken = this.authService.decodeJwtToken(token);
        this.authService.setToken(token);
        if (decodedToken?.role) {
          this.authService.setUserRole(decodedToken.role);
        }
        this.router.navigate(['/dashboard']);
      } else {
        this.loginError = 'No token received. Please try again.';
      }
    });
  }

  // Method to toggle password visibility
  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}
