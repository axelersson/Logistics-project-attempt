import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './auth.guard'; // Import the guard
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', component: HomeComponent }, // Default route
  { path: 'login', component: LoginComponent }, // Default route
  //{ path: 'feature', loadChildren: () => import('./feature/feature.module').then(m => m.FeatureModule), canActivate: [AuthGuard] }, Add canActivate: [AuthGuard] to all other routes except 'home'
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
