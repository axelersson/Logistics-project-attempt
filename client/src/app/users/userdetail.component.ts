import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/userService';

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.css'],
})
export class UserdetailComponent implements OnInit {
  selectedUsername: string = '';
  selectedUserPassword: string = '';
  selectedUserRole: string = '';

  usernames: string[] = []; // Will be populated from the API
  roles: string[] = ['Admin', 'User']; // Assuming these are static

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userService.usernames$.subscribe((usernames) => {
      this.usernames = usernames; // Assign the fetched usernames
    });
    this.userService.fetchAllUsernames(); // Fetch usernames from the API
  }

  onUsernameSelect(): void {
    console.log(`Selected username: ${this.selectedUsername}`);
  }

  selectUser(): void {
    console.log(`User ${this.selectedUsername} selected`);
  }

  deleteUser(): void {
    console.log(`Deleting user ${this.selectedUsername}`);
  }

  editUser(): void {
    console.log(
      `Editing user ${this.selectedUsername} with role ${this.selectedUserRole}`,
    );
  }

  createUser(): void {
    console.log(`Creating user with username: ${this.selectedUsername}`);
  }
}
