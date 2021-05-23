import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './common/helpers/auth.guard';
import { LoginComponent } from './core/components/authentication/login/login/login.component';
import { GroupManagementComponent } from './core/components/group-management/group-management.component';
import { EditUserComponent } from './core/components/user-management/edit-user/edit-user.component';
import { UserManagementComponent } from './core/components/user-management/user-management/user-management.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {path:'user-management', component:UserManagementComponent, canActivate:[AuthGuard]},
  {path:'edit-user', component:EditUserComponent, canActivate:[AuthGuard]},
  {path:'group-management', component:GroupManagementComponent, canActivate:[AuthGuard]},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
