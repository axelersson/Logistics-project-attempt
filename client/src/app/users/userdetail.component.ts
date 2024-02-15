import { Component } from '@angular/core';

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.css'] // Corrected from 'styleUrl' to 'styleUrls'
})
export class UserdetailComponent {
  // Properties bound to the form fields
  selectedUsername: string;
  selectedUserPassword: string;
  selectedUserRole: string;

  // Sample data for usernames and roles - replace with actual data source
  usernames: string[] = ['User1', 'User2', 'User3'];
  roles: string[] = ['Admin', 'Editor', 'Viewer'];

  constructor() {}

  // Method called when a username is selected from the dropdown
  onUsernameSelect(): void {
    console.log(`Selected username: ${this.selectedUsername}`);
    // Additional logic to fetch and display the user's details can be added here
  }

  // Method to handle user selection action
  selectUser(): void {
    console.log(`User ${this.selectedUsername} selected`);
    // Implement the logic to select the user here
  }

  // Method to handle user deletion
  deleteUser(): void {
    console.log(`Deleting user ${this.selectedUsername}`);
    // Implement the logic to delete the user here
  }

  // Method to handle user information editing
  editUser(): void {
    console.log(`Editing user ${this.selectedUsername} with role ${this.selectedUserRole}`);
    // Implement the logic to update the user's details here
  }

  // Method to handle new user creation
  createUser(): void {
    console.log(`Creating user with username: ${this.selectedUsername}`);
    // Implement the logic to create a new user here
  }
}
