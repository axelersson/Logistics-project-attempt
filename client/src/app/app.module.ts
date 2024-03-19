import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Ensure FormsModule is imported for ngModel
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule, MatIconButton } from '@angular/material/button';

import { MatToolbarModule } from '@angular/material/toolbar';
import { HomeComponent } from './home/home.component';
import { OrderdetailComponent } from './orderdetail/orderdetail.component';
import { LogoutComponent } from './logout/logout.component';
import { LocationListComponent } from './location-list/location-list.component';
import { MatCardModule } from '@angular/material/card';
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

import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { AreaDetailsComponent } from './area/area-detail/area-detail.component';
import { MatDialogModule } from '@angular/material/dialog';

// import { MatInputModule } from '@angular/material/input';
// import { MatSelectModule } from '@angular/material/select';

import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AdminlocationComponent } from './adminlocation/adminlocation.component';
import { MatOptionModule } from '@angular/material/core';
import { UserlocationComponent } from './userlocation/userlocation.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdmineditComponent } from './adminedit/adminedit.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { API_BASE_URL } from './services/api';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { TruckPageComponent } from './Trucks/truck-page/truck-page.component';
import { CreateTruckComponent } from './Trucks/create-truck/create-truck.component';

import { JwtInterceptor } from './services/jwt-interceptor';
import { AdminorderComponent } from './adminorder/adminorder.component';
import { CompleteorderComponent } from './completeorder/completeorder.component';
import { CreateorderComponent } from './createorder/createorder.component';
import { DisplayorderComponent } from './displayorder/displayorder.component';
import { UpdateorderComponent } from './updateorder/updateorder.component';
import { SortByStatusPipe } from './pipe/sort-by-status.pipe';

import { TrucklistComponent } from './Trucks/trucklist/trucklist.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavigationComponent,
    HomeComponent,
    //OrderdetailComponent,
    LogoutComponent,
    LocationListComponent,
    TruckPageComponent,
    LocationListComponent,
    UserdetailComponent,
    UsercreateComponent,
    AreaComponent,
    AreaCrudpageComponent,
    CreateareaComponent,
    CreateTruckComponent,
    ConfirmationDialogComponent,
    AreaDetailsComponent,
    ConfirmDialogComponent,

    AppComponent, // Declare it here
    LoginComponent, // Declare other components here
    NavigationComponent, // ... any other components

    LocationListComponent,
    AdminlocationComponent,
    UserlocationComponent,
    AdmineditComponent,
    CreateTruckComponent,
    
     
    AdminorderComponent,
    CompleteorderComponent,
    CreateorderComponent,
    DisplayorderComponent,
    UpdateorderComponent,
    SortByStatusPipe,

    TrucklistComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, 
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    HttpClientModule,
    MatSlideToggleModule,
    MatButtonModule,

    MatToolbarModule,
    MatIconButton,
    MatCardModule,
    // Include the Angular Material modules
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
    MatIconModule,

    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatOptionModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatSnackBarModule,
  ],
  providers: [
    Client,
    DummyDataService,
    { provide: API_BASE_URL, useValue: 'http://localhost:5000' },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    provideAnimationsAsync(),
  ], // Provide your services here
  bootstrap: [AppComponent], // Bootstrap the AppComponent
})
export class AppModule {}
