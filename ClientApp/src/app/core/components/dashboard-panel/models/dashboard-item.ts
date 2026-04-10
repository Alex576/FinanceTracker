import { GridsterItemConfig } from "angular-gridster2";

export interface DashboardItem extends GridsterItemConfig {
    id: string;
    name: string;
    fields: DashboardField[];
}

export interface DashboardField {
    name: string;
    value: unknown;
}