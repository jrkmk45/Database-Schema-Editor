import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainCanvasComponent } from './components/main-canvas/main-canvas.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CanvasPageComponent } from './pages/canvas-page/canvas-page.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { TableComponent } from './components/table/table.component';
import { CreateTablePopupComponent } from './components/popups/create-table-popup/create-table-popup.component';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { EditTablePopupComponent } from './components/popups/edit-table-popup/edit-table-popup.component';
import { MatSelectModule } from '@angular/material/select';
import { SchemeListComponent } from './components/scheme-list/scheme-list.component';
import { HeaderComponent } from './components/header/header.component';
import { UserInfoBlockComponent } from './components/user-info-block/user-info-block.component';
import { SchemeListPageComponent } from './pages/scheme-list-page/scheme-list-page.component';
import { CreateSchemeComponent } from './components/create-scheme/create-scheme.component';

@NgModule({
  declarations: [
    AppComponent,
    MainCanvasComponent,
    CanvasPageComponent,
    LoginComponent,
    RegisterComponent,
    LoginPageComponent,
    RegisterPageComponent,
    TableComponent,
    CreateTablePopupComponent,
    EditTablePopupComponent,
    SchemeListComponent,
    HeaderComponent,
    UserInfoBlockComponent,
    SchemeListPageComponent,
    CreateSchemeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDialogModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatSelectModule
  ],
  providers: [ 
    {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
    }, 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
