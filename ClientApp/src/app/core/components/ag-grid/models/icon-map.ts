import { RowAction } from "./row-action";

export const IconMap = new Map<RowAction, string>([
    [RowAction.Edit, 'edit'],
    [RowAction.Show, 'mystery'],
    [RowAction.Remove, 'delete'],
]);