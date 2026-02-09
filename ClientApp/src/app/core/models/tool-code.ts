export enum ToolCode {
    Home = 1,
    Finances = 2,
}

export const ToolCodeUrlMap = new Map<ToolCode, string>([
    [ToolCode.Home, 'home'],
    [ToolCode.Finances, 'finances'],

]);