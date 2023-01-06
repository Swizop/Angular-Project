import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { DashboardModule } from './modules/dashboard/dashboard.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterModule } from './modules/register/register.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DashboardModule,
    RegisterModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
