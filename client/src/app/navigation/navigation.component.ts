import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent implements OnInit {
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;

  constructor(
    private authService: AuthService,
    private location: Location,
  ) {}

  ngOnInit(): void {
    this.authService.isLoggedInObservable.subscribe((status) => {
      this.isLoggedIn = status;
      // Here you can also set isAdmin based on the decoded token if needed
    });
  }

  logout(): void {
    this.authService.logout();
  }

  goBack(): void {
    this.location.back();
  }
}
