import { Injectable } from '@angular/core';
import { Client } from './api'; // Adjust the import path as necessary

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private client: Client) {}

/*   logAllUsernames() {
    this.client.getAllUsernames().subscribe({
      next: (response) => {
        if (response.usernames && response.usernames.length > 0) {
          response.usernames.forEach(username => {
            console.log(username);
          });
        } else {
          console.log('No usernames found');
        }
      },
      error: (error) => {
        console.error('Error fetching usernames:', error);
      }
    });
  } */
  logAllUsernames() {
    this.client.getAllUsernames().subscribe({
      next: (response) => {
        console.log('API response:', response?.usernames);
      },
      error: (error) => {
        console.error('Error fetching usernames:', error);
      }
    });
  }
  
}
