import { ChangeDetectionStrategy, Component, computed, input, signal, untracked } from '@angular/core';
import { FlatTreeEntity, TreeFlatItem } from '../../models/tree-flat-item-model';

@Component({
  selector: 'app-tree-view',
  templateUrl: './tree-flat-view.component.html',
  styleUrls: ['./tree-flat-view.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export abstract class TreeFlatViewComponent<T extends FlatTreeEntity> {
  readonly items = input.required<T[]>();

  protected readonly flatItemMap = new Map<number, TreeFlatItem<T>>();

  protected readonly selectedItem = signal<number>(null);
  private readonly changedItem = signal<TreeFlatItem<T>>(null, { equal: () => false });
  protected readonly flatItems = computed<TreeFlatItem<T>[]>(() => this.prepareFlatItems(this.items()));
  protected readonly visibleItems = computed<TreeFlatItem<T>[]>(() => this.getVisibleItems(this.flatItems(), this.changedItem()));

  expand(item: TreeFlatItem<T>): void {
    item.expanded = !item.expanded;
    this.changedItem.set(item);
  }

  private prepareFlatItems(items: T[]): TreeFlatItem<T>[] {
    const itemsMap = new Map<number, FlatTreeEntity>(items.map((item) => [item.id, item]));
    const parentItemsSet = new Set<number>(items.filter(x => x.parentId).map((item) => item.parentId));
    const levelMap = new Map<number, number>();

    const getLevel = (itemId: number): number => {
      const item = itemsMap.get(itemId);
      if (!item.parentId) {
        return 0;
      }

      const savedLevel = levelMap.get(item.id);
      if (savedLevel) {
        return savedLevel;
      }

      const level = getLevel(item.parentId) + 1;
      levelMap.set(itemId, level);
      return level;
    };

    const convert = (item: T): TreeFlatItem<T> => {
      const level = getLevel(item.id);
      const flatItem: TreeFlatItem<T> = {
        data: item,
        level: level,
        hasChild: parentItemsSet.has(item.id)
      };
      this.flatItemMap.set(item.id, flatItem);
      return flatItem;
    };

    const flatItems = items.map((item) => convert(item));
    this.expandSelectedItem();
    return flatItems;
  }

  private expandSelectedItem(): void {
    const selectedItem = untracked(() => this.selectedItem());
    if (selectedItem != null) {
      let item = this.flatItemMap.get(selectedItem);
      while (item.data.parentId) {
        item = this.flatItemMap.get(item.data.parentId);
        item.expanded = true;
      }
    }
  }

  private getVisibleItems(items: TreeFlatItem<T>[], changedItem: TreeFlatItem<T>): TreeFlatItem<T>[] {
    const result: TreeFlatItem<T>[] = [];
    for (const item of items) {
      if (item.level === 0 || (item.data.parentId && this.flatItemMap.get(item.data.parentId).expanded)) {
        result.push(item);
      }
    }
    return result;
  }
}
