import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

const routes: Routes = [
    { path: '', redirectTo: 'activity/home', pathMatch: 'prefix' },
    { path: 'activity', loadChildren: './activity/activity.module#ActivityModule' }
];

@NgModule({
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
