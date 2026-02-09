import { Routes } from "@angular/router";
import { RouteData } from "../models/route-data";
import { ToolCode } from "../models/tool-code";
import { WorkplaceComponent } from "../workplace/workplace.component";

export const routes: Routes = [
    {
        path: 'workplace',
        component: WorkplaceComponent,

        children: [
            {
                path: 'home',
                data: { [RouteData.ToolCode]: ToolCode.Home },
                loadComponent: () => import('../components/home/home.component').then(c => c.HomeComponent)
            },
            {
                path: 'finances',
                data: { [RouteData.ToolCode]: ToolCode.Finances },
                loadComponent: () => import('../components/finances/finances.component').then(c => c.FinancesComponent)
            },
        ]
    },
    {
        path: '',
        redirectTo: '/workplace/home',
        pathMatch: 'full'
    },
];
