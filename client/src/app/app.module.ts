import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule if you use Template-driven forms
import { AppRoutingModule } from './app-routing.module'; // Import the AppRoutingModule
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component'; // Adjust path as necessary
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { Client } from './services/api';
import { HttpClientModule } from '@angular/common/http';
import { LocationListComponent } from './location-list/location-list.component';
import { MatCardModule } from '@angular/material/card';
<<<<<<< Updated upstream
import { MatButtonModule } from '@angular/material/button';
=======
import { ChooseTruckComponent } from './choose-truck/choose-truck.component';
import { UserdetailComponent } from './users/userdetail/userdetail.component';
// Import Angular Material modules

import { Client } from './services/api';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { UsercreateComponent } from './users/usercreate/usercreate.component';
import { AreaComponent } from './area/area.component';
import { AreaCrudpageComponent } from './area/area-crudpage/area-crudpage.component';
import { MatIconModule } from '@angular/material/icon';
import { DummyDataService } from './dummy-data.service';
import { CreateareaComponent } from './area/createarea/createarea.component';
import { ViewTruckComponent } from './view-truck/view-truck.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { AreaDetailsComponent } from './area/area-detail/area-detail.component';
import { MatDialogModule } from '@angular/material/dialog';

// import { MatInputModule } from '@angular/material/input';
// import { MatSelectModule } from '@angular/material/select';

>>>>>>> Stashed changes
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AdminlocationComponent } from './adminlocation/adminlocation.component';
import { MatOptionModule } from '@angular/material/core';
import { UserlocationComponent } from './userlocation/userlocation.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdmineditComponent } from './adminedit/adminedit.component';
<<<<<<< Updated upstream

@NgModule({
  declarations: [
=======
import { FlexLayoutModule } from '@angular/flex-layout';
import { API_BASE_URL } from './services/api';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavigationComponent,
    HomeComponent,
    OrderdetailComponent,
    LogoutComponent,
    LocationListComponent,
    ChooseTruckComponent,
    LocationListComponent,
    UserdetailComponent,
    UsercreateComponent,
    AreaComponent,
    AreaCrudpageComponent,
    CreateareaComponent,
    ViewTruckComponent,
    ConfirmationDialogComponent,
    AreaDetailsComponent,
    ConfirmDialogComponent,
>>>>>>> Stashed changes
    AppComponent, // Declare it here
    LoginComponent, // Declare other components here
    NavigationComponent, // ... any other components
    DashboardComponent,
    LocationListComponent,
    AdminlocationComponent,
    UserlocationComponent,
    AdmineditComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    HttpClientModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatOptionModule,
    BrowserAnimationsModule
  ],
<<<<<<< Updated upstream
  providers: [Client],
  bootstrap: [AppComponent], // Bootstrap AppComponent
=======
  providers: [Client, DummyDataService,{ provide: API_BASE_URL, useValue: 'http://localhost:5000' }], // Provide your services here
  bootstrap: [AppComponent], // Bootstrap the AppComponent
>>>>>>> Stashed changes
})
export class AppModule {}
