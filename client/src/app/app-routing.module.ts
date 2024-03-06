import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './security/auth.guard'; // Import the guard
import { LoginComponent } from './login/login.component';
import { OrderdetailComponent } from './orderdetail/orderdetail.component';
import { LogoutComponent } from './logout/logout.component';
import { LocationListComponent } from './location-list/location-list.component';

import { AdminlocationComponent } from './adminlocation/adminlocation.component';
import { UserlocationComponent } from './userlocation/userlocation.component';
import { AdmineditComponent } from './adminedit/adminedit.component';

import { UserdetailComponent } from './users/userdetail/userdetail.component';
import { UsercreateComponent } from './users/usercreate/usercreate.component';
import { Area } from './services/api';
import { AreaComponent } from './area/area.component';
import { AreaCrudpageComponent } from './area/area-crudpage/area-crudpage.component';
import { CreateareaComponent } from './area/createarea/createarea.component';
import { AreaDetailsComponent } from './area/area-detail/area-detail.component';
import { TruckPageComponent } from './Trucks/truck-page/truck-page.component';
import { CreateTruckComponent } from './Trucks/create-truck/create-truck.component';

import { AdminorderComponent } from './adminorder/adminorder.component';
import { CompleteorderComponent } from './completeorder/completeorder.component';
import { CreateorderComponent } from './createorder/createorder.component';
import { DisplayorderComponent } from './displayorder/displayorder.component';
import { UpdateorderComponent } from './updateorder/updateorder.component';

const routes: Routes = [
  { path: '', redirectTo: '/homepage', pathMatch: 'full' }, // default route
  { path: 'adminedit', component: AdmineditComponent },
  { path: 'adminlocation', component: AdminlocationComponent },
  { path: 'adminorder', component: AdminorderComponent },
  { path: 'area/:areaId', component: AreaDetailsComponent },
  { path: 'areacrud', component: AreaCrudpageComponent },
  { path: 'arealist', component: AreaComponent },
  { path: 'createarea', component: CreateareaComponent },
  { path: 'createorder', component: CreateorderComponent },
  { path: 'createtruck', component: CreateTruckComponent },
  { path: 'completeorder', component: CompleteorderComponent },
  { path: 'displayorder', component: DisplayorderComponent },
  { path: 'homepage', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'locationlist', component: LocationListComponent },
  { path: 'orderlist', component: OrderdetailComponent },
  { path: 'truckpage', component: TruckPageComponent },
  { path: 'updateorder', component: UpdateorderComponent },
  {
    path: 'usercreate',
    component: UsercreateComponent,
    canActivate: [AuthGuard],
    data: { role: 'Admin' },
  },
  {
    path: 'userdetail',
    component: UserdetailComponent,
    canActivate: [AuthGuard],
    data: { role: 'Admin' },
  },
  { path: 'userlocation', component: UserlocationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
