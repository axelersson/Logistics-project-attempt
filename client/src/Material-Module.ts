import { NgModule } from "@angular/core";
import { NgModel } from "@angular/forms";
import {MatCardModule} from "@angular/material/card"
import {MatInputModule} from "@angular/material/input"
import {MatButtonModule} from "@angular/material/button"
import {MatFormFieldModule} from "@angular/material/form-field"
import {MatTableModule} from "@angular/material/table"


@NgModule({
      exports:[
            MatCardModule,
            MatFormFieldModule,
            MatInputModule,
            MatButtonModule,
            MatTableModule
      ]
})
export class MaterialModule{

}