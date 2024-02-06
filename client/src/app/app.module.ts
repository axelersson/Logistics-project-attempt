import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule if you use Template-driven forms
import { AppRoutingModule } from './app-routing.module'; // Import the AppRoutingModule
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component'; // Import HomeComponent

@NgModule({
  declarations: [
    AppComponent, // Declare it here
    LoginComponent, // Declare other components here
    HomeComponent,
    // ... any other components
  ],
  imports: [BrowserModule, FormsModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent], // Bootstrap AppComponent
})
export class AppModule {}
