import { Component } from '@angular/core';
import {Client, Area} from '../../services/api'
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-createarea',
  templateUrl: './createarea.component.html',
  styleUrl: './createarea.component.css'
})

export class CreateareaComponent {
  area: Area;
  constructor(private client: Client, private router: Router, private snackBar: MatSnackBar) {
    this.area = new Area();
    this.area.areaId = "";
    this.area.name = ""
  }

  confirm() : void{
    if ( this.area.areaId == "" || this.area.name == ""){
      const snackBarRef = this.snackBar.open('Please complete all fields', 'Close', { duration: 500 });
    setTimeout(() => {
      snackBarRef.dismiss();
    }, 500);
    return;
    } else {
      this.client.areasPOST(this.area).subscribe(
        (response:Area) => {
          console.log('area posted', response)
        }, 
        (error) => {
          console.log(error)
        }
      );
    }
  }
  updateArea(): void {
    if ( this.area.areaId == "" || this.area.name == ""){
      const snackBarRef = this.snackBar.open('Please complete all fields', 'Close', { duration: 500 });
    setTimeout(() => {
      snackBarRef.dismiss();
    }, 500);
    this.client.areasPUT(this.area.areaId ?? '', this.area).subscribe(
      (response) => {
        console.log('Area updated successfully', response);
        this.snackBar.open('Area updated successfully', 'Close', { duration: 500 });
      },
      (error: any) => {
        console.error('Error updating area:', error);
        this.snackBar.open('Could not update area, check that the area ID is correct.', 'Close', { duration: 500 });
      }
    );
  }
}
}
