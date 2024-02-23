import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent implements OnInit {
  isAdmin: boolean = false;

  constructor(private authService: AuthService, private location: Location) {}

  ngOnInit(): void {
    // Logic to determine if the user is an admin
    // For now, let's just set it to true for demonstration purposes
    this.isAdmin = true; // Or dynamically set based on user role
  }

  logout(): void {
    this.authService.logout();
  }

  goBack(): void {
    this.location.back();
  }
}
