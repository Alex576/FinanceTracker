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
                loadComponent: () => import('../components/capitals/capitals.component').then(c => c.CapitalsComponent)
            },
            {
                path: 'users',
                data: { [RouteData.ToolCode]: ToolCode.Users },
                loadComponent: () => import('../components/users/users.component').then(c => c.UsersComponent)
            },
            {
                path: 'roles',
                data: { [RouteData.ToolCode]: ToolCode.Roles },
                loadComponent: () => import('../components/roles/roles.component').then(c => c.RolesComponent)
            },
            {
                path: 'translations',
                data: { [RouteData.ToolCode]: ToolCode.Translation },
                loadComponent: () => import('../components/translations/translations.component').then(c => c.TranslationsComponent)
            },
            {
                path: 'layout',
                data: { [RouteData.ToolCode]: ToolCode.Layout },
                loadComponent: () => import('../components/layout-editor/layout-editor.component').then(c => c.LayoutEditorComponent)
            },
        ]
    },
    {
        path: '',
        redirectTo: '/workplace',
        pathMatch: 'full'
    },
];
