import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { routes } from './offers.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { ProjectOffersComponent } from './project-offers/project-offers.component';
import { CleanDatePipe } from 'src/app/pipes/clean-date.pipe';



@NgModule({
  declarations: [
    ProjectOffersComponent,
    CleanDatePipe
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  exports: [
    CleanDatePipe
  ]
})
export class OffersModule { }
