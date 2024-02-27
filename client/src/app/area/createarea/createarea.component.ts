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

  cancel() : void {
    console.log("cancerl")
  }
}

// class Area {
//   areaId: string;
//   name: string;

//   constructor(areaId: string, name: string) {
//       this.areaId = areaId;
//       this.name = name;
//   }
// }
