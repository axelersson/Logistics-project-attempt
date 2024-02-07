import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  // Simulate a login by storing an auth token in local storage
  login(username: string, password: string): boolean {
    // In a real application, you would make an HTTP request to your backend here
    // For demonstration, we'll just simulate successful login
    const fakeToken = '123456789'; // This should come from your authentication backend
    localStorage.setItem('authToken', fakeToken);
    return true;
  }

  // Check if the user is logged in
  isLoggedIn(): boolean {
    const token = localStorage.getItem('authToken');
    return !!token;
  }

  // Log out the user by removing the auth token
  logout(): void {
    localStorage.removeItem('authToken');
  }
}
