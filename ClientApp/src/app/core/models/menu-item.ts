import { ToolCode } from "./tool-code";
import { FlatTreeEntity } from "./tree-flat-item-model";

export interface MenuItem extends FlatTreeEntity {
    // code: MenuCode;
    toolCode: ToolCode;
    name: string;
    // parentId?: MenuCode;
    icon: string;
}

export enum MenuCode {
    Dashboard = 1,
    Finances = 2,
    Settings = 3,
    Roles = 4,
    Users = 5,
    Translation = 6,
    Layout = 7,

}

export const MenuCodeIcon = new Map<MenuCode, string>([
    [MenuCode.Dashboard, 'dashboard'],
    [MenuCode.Finances, 'savings'],
    [MenuCode.Settings, 'settings'],
    [MenuCode.Roles, 'admin_panel_settings'],
    [MenuCode.Users, 'person'],
    [MenuCode.Translation, 'translate'],
    [MenuCode.Layout, 'responsive_layout'],
]);

export const ToolCodeUrlMap = new Map<ToolCode, string>([
    [ToolCode.Dashboard, '/workplace/dashboard'],
    [ToolCode.Finances, '/workplace/finances'],
    [ToolCode.Roles, '/workplace/roles'],
    [ToolCode.Users, '/workplace/users'],
    [ToolCode.Translation, '/workplace/translations'],
    [ToolCode.Layout, '/workplace/layout'],

]);