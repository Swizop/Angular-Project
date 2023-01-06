import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user/user.component';
import { LayoutComponent } from './layout/layout.component';
import { ConstructorComponent } from './constructor/constructor.component';
import { RouterModule } from '@angular/router';
import { routes } from './register.routes';



@NgModule({
  declarations: [
    UserComponent,
    LayoutComponent,
    ConstructorComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class RegisterModule { }
