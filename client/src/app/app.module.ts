// app.module.ts

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    // Import other modules here
  ],
  providers: [],
  // declarations and bootstrap arrays are removed
})
export class AppModule {}
