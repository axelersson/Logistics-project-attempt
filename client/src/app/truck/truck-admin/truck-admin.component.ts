import { Component } from '@angular/core';

@Component({
  selector: 'app-truck-admin',
  templateUrl: './truck-admin.component.html',
  styleUrls: ['./truck-admin.component.css']
})
export class TruckAdminComponent {

  constructor() { }

  bookTruck() {
    console.log('Book truck');
    // Placeholder for the book truck logic
  }

  createTruck() {
    console.log('Create truck');
    // Placeholder for the create truck logic
  }

  deleteTruck() {
    console.log('Delete truck');
    // Placeholder for the delete truck logic
  }

  updateTruck() {
    console.log('Update truck');
    // Placeholder for the update truck logic
  }

  viewTruck() {
    console.log('View truck');
    // Placeholder for the view truck logic
  }

  cancel() {
    console.log('Cancel action');
    // Placeholder for cancel logic, possibly navigate back
  }
}
