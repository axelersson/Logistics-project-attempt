import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userService';
import { UserIdAndRoleResponseFromUsernameRequest } from '../../services/api';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.css'],
})
export class UserdetailComponent implements OnInit {
  selectedUsername: string = '';
  selectedUserPassword: string = '';
  selectedUserRole: string = '';
  confirmedPassword: string = '';
  originalUserRole: string = '';
  passwordsMatch: boolean = true;
  userDetailsFetched: boolean = false;

  usernames: string[] = [];
  roles: string[] = ['Admin', 'User'];
  userDetails: UserIdAndRoleResponseFromUsernameRequest | null = null;

  constructor(
    private userService: UserService,
    public dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.userService.usernames$.subscribe(usernames => this.usernames = usernames);
    this.userService.fetchAllUsernames();
  }

  checkPasswords(): void {
    this.passwordsMatch = this.selectedUserPassword === this.confirmedPassword;
  }

  onUsernameSelect(): void {
    this.userDetailsFetched = false;
    this.originalUserRole = ''; // Reset originalUserRole on new selection
  }

  selectUser(): void {
    this.originalUserRole = this.userDetails?.role || '';
    if (this.selectedUsername) {
      this.userService.fetchUserDetailsByUsername(this.selectedUsername);
      // Assuming fetchUserDetailsByUsername updates selectedUserDetails$ Observable
      this.userService.selectedUserDetails$.subscribe(
        (details: UserIdAndRoleResponseFromUsernameRequest | null) => {
          if (details) {
            this.userDetails = details;
            this.selectedUserRole = details.role || ''; // Provide a default if undefined
            console.log(this.userDetails);
            this.userDetailsFetched = true;
          }
        },
      );
    }
    console.log(`User ${this.selectedUsername} selected`);
  }

  deleteUser() {
    console.log('Delete user functionality here');
  }

  createUser() {
    this.router.navigate(['/usercreate']);
  }

  editUser() {
    console.log('Edit user functionality here');
  }

  confirmDelete(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: { message: 'Are you sure you want to delete this user?' },
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        console.log('User deletion confirmed');
        // Implement deletion logic here
      }
    });
  }
}
