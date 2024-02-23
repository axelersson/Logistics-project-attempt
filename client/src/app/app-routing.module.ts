import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './auth.guard'; // Import the guard
import { LoginComponent } from './login/login.component';
import { OrderdetailComponent } from './orderdetail/orderdetail.component';
import { LogoutComponent } from './logout/logout.component';
import { LocationListComponent } from './location-list/location-list.component';
import { AdminlocationComponent } from './adminlocation/adminlocation.component';
import { UserlocationComponent } from './userlocation/userlocation.component';
import { AdmineditComponent } from './adminedit/adminedit.component';
import { ChooseTruckComponent } from './choose-truck/choose-truck.component';
import { UserdetailComponent } from './users/userdetail/userdetail.component';
import { UsercreateComponent } from './users/usercreate/usercreate.component';
import { Area } from './services/api';
import { AreaComponent } from './area/area.component';
import { AreaCrudpageComponent } from './area/area-crudpage/area-crudpage.component';
import { CreateareaComponent } from './area/createarea/createarea.component';
import { ViewTruckComponent } from './view-truck/view-truck.component'; // Import the new component
import { AreaDetailsComponent } from './area/area-detail/area-detail.component';



const routes: Routes = [
  { path: '', redirectTo: '/homepage', pathMatch: 'full' }, // default route
  { path: 'login', component: LoginComponent }, // Default route
  { path: 'logout', component: LogoutComponent },
  { path: 'locationlist', component: LocationListComponent },
  { path: 'adminlocation', component: AdminlocationComponent},
  { path: 'userlocation' , component: UserlocationComponent},
  { path: 'adminedit', component: AdmineditComponent},
  //{ path: 'feature', loadChildren: () => import('./feature/feature.module').then(m => m.FeatureModule), canActivate: [AuthGuard] }, Add canActivate: [AuthGuard] to all other routes except 'home'
  //{ path: 'feature', loadChildren: () => import('./feature/feature.module').then(m => m.FeatureModule), canActivate: [AuthGuard] }, Add canActivate: [AuthGuard] to all other routes except 'home',
  { path: 'userdetail', component: UserdetailComponent },
  { path: 'usercreate', component: UsercreateComponent },
  { path: 'view-truck/:id', component: ViewTruckComponent }, 
  { path: 'area/:areaId', component: AreaDetailsComponent },
  { path: 'homepage', component:HomeComponent},
  { path: 'arealist', component:AreaComponent},
  { path: 'orderlist', component:OrderdetailComponent},
  { path: 'choosetruck', component:ChooseTruckComponent},
  { path: 'usercrud', component:AreaCrudpageComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
