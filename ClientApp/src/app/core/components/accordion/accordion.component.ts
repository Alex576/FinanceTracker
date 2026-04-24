import { CdkAccordionModule } from '@angular/cdk/accordion';
import { ChangeDetectionStrategy, Component, computed, effect, input, linkedSignal, output } from '@angular/core';
import { MatIcon } from "@angular/material/icon";

@Component({
  selector: 'app-accordion',
  templateUrl: './accordion.component.html',
  styleUrls: ['./accordion.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CdkAccordionModule, MatIcon],
})
export class AccordionComponent {
  readonly initOpenState = input<boolean>(false);

  readonly opened = output<void>();

  protected readonly closed = linkedSignal<boolean>(() => !this.initOpenState());
  protected readonly contentClassState = computed<string[]>(() => this.closed() ? ['hidden'] : ['show']);

  constructor() {
    effect(() => {
      if (!this.closed()) {
        this.opened.emit();
      }
    });
  }

  onToggle(): void {
    this.closed.update(x => !x);
  }
}
