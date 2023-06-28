import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CanvasPageComponent } from './pages/canvas-page/canvas-page.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { SchemeListPageComponent } from './pages/scheme-list-page/scheme-list-page.component';

const routes: Routes = [
  { path:'', component: SchemeListPageComponent, canActivate: [AuthGuard] },
  { path:'scheme/:id', component: CanvasPageComponent, canActivate: [AuthGuard] },
  { path:'login', component: LoginPageComponent },
  { path:'register', component: RegisterPageComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
