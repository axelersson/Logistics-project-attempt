import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Ensure FormsModule is imported for ngModel
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HomeComponent } from './home/home.component';
import { OrderdetailComponent } from './orderdetail/orderdetail.component';
import { LogoutComponent } from './logout/logout.component';
import { LocationListComponent } from './location-list/location-list.component';
import { MatCardModule } from '@angular/material/card';
import { TruckListComponent } from './choose-truck/choose-truck.component';
import { UserdetailComponent } from './users/userdetail/userdetail.component';
// Import Angular Material modules
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Import BrowserAnimationsModule
import { Client } from './services/api';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { UsercreateComponent } from './users/usercreate/usercreate.component';
import { AreaComponent } from './area/area.component';
import { AreaCrudpageComponent } from './area/area-crudpage/area-crudpage.component';
import {MatIconModule} from '@angular/material/icon';
import { DummyDataService } from './dummy-data.service';
import { CreateareaComponent } from './area/createarea/createarea.component';
import { ViewTruckComponent } from './view-truck/view-truck.component';
// import { MatInputModule } from '@angular/material/input';
// import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavigationComponent,
    HomeComponent,
    OrderdetailComponent,
    LogoutComponent,
    LocationListComponent,
    TruckListComponent,
    LocationListComponent,
    UserdetailComponent,
    UsercreateComponent,
    AreaComponent,
    AreaCrudpageComponent,
    CreateareaComponent,
    ViewTruckComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, // Include BrowserAnimationsModule
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    HttpClientModule,
    MatSlideToggleModule,
    MatButtonModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatCardModule,
    MatIconButton,
    // Include the Angular Material modules
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
    MatIconModule,
  ],
  providers: [Client, DummyDataService], // Provide your services here
  bootstrap: [AppComponent], // Bootstrap the AppComponent
})
export class AppModule {}
