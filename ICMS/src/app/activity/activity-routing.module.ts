import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivityComponent } from './activity.component';
import { SelectActivityComponent } from './select-activity/select-activity.component';
import { ActivityDetailComponent } from './activity-detail/activity-detail.component';

const routes: Routes = [
    {
        path: '',
        component: ActivityComponent,
        children: [
          { path: '', redirectTo: 'home', pathMatch: 'prefix' },
            { path: 'home', component: SelectActivityComponent },
            { path: 'detail/:id/:studentId', component: ActivityDetailComponent },
            { path: 'detail/:id', component: ActivityDetailComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ActivityRoutingModule {}
