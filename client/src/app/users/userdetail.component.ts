import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/userService';
import { UserIdAndRoleResponseFromUsernameRequest } from '../services/api'; // Adjust path as necessary
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component'; // Adjust the path as necessary

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.css'],
})
export class UserdetailComponent implements OnInit {
  selectedUsername: string = '';
  selectedUserPassword: string = '';
  selectedUserRole: string = '';

  usernames: string[] = [];
  roles: string[] = ['Admin', 'User'];
  userDetails: UserIdAndRoleResponseFromUsernameRequest | null = null;
  isUserDetailsFetched: boolean = false;

  constructor(
    private userService: UserService,
    public dialog: MatDialog,
  ) {}

  ngOnInit() {
    this.userService.usernames$.subscribe((usernames) => {
      this.usernames = usernames;
    });
    this.userService.fetchAllUsernames();
  }

  onUsernameSelect(): void {
    this.isUserDetailsFetched = false; // Reset the flag
    console.log(`Selected username: ${this.selectedUsername}`);
    // Disable UI elements here if needed
  }

  selectUser(): void {
    if (this.selectedUsername) {
      this.userService.fetchUserDetailsByUsername(this.selectedUsername);
      // Assuming fetchUserDetailsByUsername updates selectedUserDetails$ Observable
      this.userService.selectedUserDetails$.subscribe(
        (details: UserIdAndRoleResponseFromUsernameRequest | null) => {
          if (details) {
            this.userDetails = details;
            this.selectedUserRole = details.role || ''; // Provide a default if undefined
            console.log(this.userDetails);
            this.isUserDetailsFetched = true;
          }
        },
      );
    }
    console.log(`User ${this.selectedUsername} selected`);
  }

  deleteUser() {
    console.log('hej');
  }
  createUser() {
    console.log('hej');
  }
  editUser() {
    console.log('hej');
  }
  confirmDelete(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: { message: 'Are you sure you want to delete this user?' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log('Yes clicked');
        // Implement your delete logic here
      }
    });
  }
}
