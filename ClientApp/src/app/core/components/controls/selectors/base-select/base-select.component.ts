import { ChangeDetectionStrategy, Component, computed } from '@angular/core';
import { ComboControlSettings } from '../../../../models/controls/combo-control-settings';
import { Item } from '../../../../models/controls/item';
import { BaseControlComponent } from '../../base-control/base-control.component';

@Component({
  selector: 'app-base-select',
  template: ``,
  styleUrls: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export abstract class BaseSelectComponent<TData> extends BaseControlComponent<TData> {
  protected readonly comboSettings = computed<ComboControlSettings>(() => this.settings() as ComboControlSettings);
  protected readonly items = computed<Item[]>(() => this.comboSettings().items);

  constructor() {
    super();

  }
}
