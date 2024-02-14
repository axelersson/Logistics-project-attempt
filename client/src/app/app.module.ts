import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule if you use Template-driven forms
import { AppRoutingModule } from './app-routing.module'; // Import the AppRoutingModule
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component'; // Adjust path as necessary
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { Client } from './services/api';
import { HttpClientModule } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HomeComponent } from './home/home.component';
import { OrderdetailComponent } from './orderdetail/orderdetail.component';
import { LogoutComponent } from './logout/logout.component';
import { AreaComponent } from './area/area.component';
import { AreaCrudpageComponent } from './area/area-crudpage/area-crudpage.component';
import {MatIconModule} from '@angular/material/icon';
import { DummyDataService } from './dummy-data.service';



@NgModule({
  declarations: [
    AppComponent, // Declare it here
    LoginComponent, // Declare other components here
    NavigationComponent, // ... any other components
    HomeComponent,
    OrderdetailComponent,
    LogoutComponent,
    AreaComponent,
    AreaCrudpageComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    HttpClientModule,
    MatSlideToggleModule,
    MatButtonModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatIconButton,
    MatButtonModule,
    MatIconModule,
  ],
  providers: [Client, DummyDataService],
  bootstrap: [AppComponent], // Bootstrap AppComponent
})
export class AppModule {}
