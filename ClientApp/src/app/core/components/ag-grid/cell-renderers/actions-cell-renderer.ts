import { ICellRendererParams } from "ag-grid-community";
import { ActionService } from "../../../services/action.service";
import { IconMap } from "../models/icon-map";
import { Row } from "../models/row";
import { RowAction } from "../models/row-action";

export function rendererActions(params: ICellRendererParams, row: Row, actionService: ActionService): HTMLElement {
    const cell = document.createElement('div');
    cell.classList.add('actions-cell');
    for (const action of row.actions) {
        const icon = document.createElement('span');
        icon.classList.add('material-symbols-outlined', 'grid-icon');
        if (action === RowAction.Remove) {
            icon.classList.add('remove-icon');
        }
        icon.innerText = IconMap.get(action);
        icon.onclick = () => {
            actionService.setAction({ action, data: row.tag });
        };

        cell.appendChild(icon);
    }

    return cell;
}