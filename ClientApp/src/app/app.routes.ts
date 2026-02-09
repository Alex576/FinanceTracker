import { Routes } from '@angular/router';
import { NotFoundComponent } from './core/components/not-found/not-found.component';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./core/routing/workplace.routes').then(w => w.routes),
    },
    // {
    //     path: '',
    //     redirectTo: '/workplace',
    //     pathMatch: 'full'
    // },
    {
        path: '**',
        component: NotFoundComponent,
    }

];
