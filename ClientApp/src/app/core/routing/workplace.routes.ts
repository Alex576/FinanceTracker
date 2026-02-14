import { Routes } from "@angular/router";
import { WorkplaceComponent } from "../components/workplace/workplace.component";
import { RouteData } from "../models/route-data";
import { ToolCode } from "../models/tool-code";

export const routes: Routes = [
    {
        path: 'workplace',
        component: WorkplaceComponent,

        children: [
            {
                path: 'dashboard',
                data: { [RouteData.ToolCode]: ToolCode.Dashboard },
                loadComponent: () => import('../components/dashboard/dashboard.component').then(c => c.DashboardComponent)
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
        redirectTo: '/workplace/dashboard',
        pathMatch: 'full'
    },
];
