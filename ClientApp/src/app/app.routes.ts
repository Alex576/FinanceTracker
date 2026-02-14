import { Routes } from '@angular/router';
import { NotFoundComponent } from './core/components/not-found/not-found.component';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./core/routing/workplace.routes').then(w => w.routes),
    },
    {
        path: 'login',
        loadComponent: () => import('./core/components/login/login.component').then(c => c.LoginComponent),
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
