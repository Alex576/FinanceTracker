export enum ToolCode {
    Dashboard = 1,
    Finances = 2,
    Settings = 3,
    Roles = 4,
    Users = 5,
}

export const ToolCodeUrlMap = new Map<ToolCode, string>([
    [ToolCode.Dashboard, 'dashboard'],
    [ToolCode.Finances, 'finances'],

]);