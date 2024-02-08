import { Component, OnInit } from '@angular/core';
// Optionally import services if you need to determine the admin status dynamically

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent implements OnInit {
  isAdmin: boolean = false; // Ensure this line is present

  constructor() {}

  ngOnInit(): void {
    // Logic to determine if the user is an admin
    // For now, let's just set it to true for demonstration purposes
    this.isAdmin = true; // Or dynamically set based on user role
  }
}
