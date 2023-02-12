import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { ProjectOffersComponent } from './project-offers/project-offers.component';

export const routes: Routes = [
    {
        path: 'project/:id',
        redirectTo: 'project/:id/offers',
        pathMatch: 'full'
    },
    {
        path: 'project/:id/offers',
        component: ProjectOffersComponent,
        canActivate: [AuthGuard]
    }
];