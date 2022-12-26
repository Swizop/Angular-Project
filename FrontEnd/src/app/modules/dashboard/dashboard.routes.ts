import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedComponent } from './feed/feed.component';

export const routes: Routes = [
    {
        path: 'dashboard',
        component: FeedComponent
    }
];

 // @NgModule({
 //   imports: [RouterModule.forRoot(routes)],
 //   exports: [RouterModule]
 // })
 // export class AppRoutingModule { }