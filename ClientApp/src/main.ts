import { bootstrapApplication } from '@angular/platform-browser';
import { AllCommunityModule as ChartCommunityModule, ModuleRegistry as ChartModuleRegistry } from 'ag-charts-community';
import { AllCommunityModule as GridCommonModule, ModuleRegistry as GridModuleRegistry } from 'ag-grid-community';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';

// Register all Community features
GridModuleRegistry.registerModules([GridCommonModule]);
ChartModuleRegistry.registerModules([ChartCommunityModule]);

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
