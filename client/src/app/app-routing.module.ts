import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './security/auth.guard'; // Import the guard
import { LoginComponent } from './login/login.component';
import { OrderdetailComponent } from './orderdetail/orderdetail.component';
import { LogoutComponent } from './logout/logout.component';
import { LocationListComponent } from './location-list/location-list.component';

import { AdminlocationComponent } from './adminlocation/adminlocation.component';
import { AdmineditComponent } from './adminedit/adminedit.component';

import { UserdetailComponent } from './users/userdetail/userdetail.component';
import { UsercreateComponent } from './users/usercreate/usercreate.component';
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
import { TrucklistComponent } from './Trucks/trucklist/trucklist.component';

const routes: Routes = [
  { path: '', redirectTo: '/homepage', pathMatch: 'full' },
  { path: 'adminedit', component: AdmineditComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'adminlocation', component: AdminlocationComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'adminorder', component: AdminorderComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'area/:areaId', component: AreaDetailsComponent, canActivate: [AuthGuard] },
  { path: 'areacrud', component: AreaCrudpageComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'arealist', component: AreaComponent, canActivate: [AuthGuard] },
  { path: 'createarea', component: CreateareaComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'createorder', component: CreateorderComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'createtruck', component: CreateTruckComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'completeorder', component: CompleteorderComponent, canActivate: [AuthGuard] },
  { path: 'displayorder', component: DisplayorderComponent, canActivate: [AuthGuard] },
  { path: 'homepage', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'locationlist', component: LocationListComponent, canActivate: [AuthGuard] },
  { path: 'orderlist', component: OrderdetailComponent, canActivate: [AuthGuard] },
  { path: 'truckpage', component: TruckPageComponent, canActivate: [AuthGuard] },
  { path: 'trucklist', component: TrucklistComponent, canActivate: [AuthGuard] },
  { path: 'updateorder', component: UpdateorderComponent, canActivate: [AuthGuard] },
  { path: 'usercreate', component: UsercreateComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'userdetail', component: UserdetailComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }

