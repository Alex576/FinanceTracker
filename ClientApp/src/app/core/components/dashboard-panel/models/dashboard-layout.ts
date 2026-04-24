import { DashboardItem } from "./dashboard-item";
import { DashboardOptions } from "./dashboard-options";

export interface DashboardLayout {
    options: DashboardOptions;
    items: DashboardItem[];
    id: number;
}