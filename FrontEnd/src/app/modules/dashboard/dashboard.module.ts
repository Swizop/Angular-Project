import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedComponent } from './feed/feed.component';
import { RouterModule } from '@angular/router';
import { routes } from './dashboard.routes';



@NgModule({
  declarations: [
    FeedComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class DashboardModule { }