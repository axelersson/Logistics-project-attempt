import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Client } from '../services/api';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

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

  faEye = faEye;
  faEyeSlash = faEyeSlash;
  hidePassword = true;

  constructor(private client: Client) {}

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  onLogin(): void {
    // Get form values
    const { username, password } = this.loginForm.value;

    // Ensure username and password are strings and not null or undefined
    const finalUsername = username ?? '';
    const finalPassword = password ?? '';

    // Call the API service to log in the user
    this.client
      .login({
        username: finalUsername,
        password: finalPassword,
      })
      .then(() => {
        // Handle successful login
        console.log('Login successful');
      })
      .catch((error) => {
        // Handle error
        console.error('Login failed:', error);
      });
  }
}
