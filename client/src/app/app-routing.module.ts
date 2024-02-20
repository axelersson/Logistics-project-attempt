import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './auth.guard'; // Import the guard
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LocationListComponent } from './location-list/location-list.component';
import { AdminlocationComponent } from './adminlocation/adminlocation.component';
import { UserlocationComponent } from './userlocation/userlocation.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }, // default route
  { path: 'dashboard', component: DashboardComponent },
  { path: 'login', component: LoginComponent }, // Default route
  { path: 'locationlist', component: LocationListComponent },
  { path: 'adminlocation', component: AdminlocationComponent},
  { path: 'userlocation' , component: UserlocationComponent}
  //{ path: 'feature', loadChildren: () => import('./feature/feature.module').then(m => m.FeatureModule), canActivate: [AuthGuard] }, Add canActivate: [AuthGuard] to all other routes except 'home'
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
