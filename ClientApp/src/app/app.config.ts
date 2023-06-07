import { ApplicationConfig } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { SearchFlightsComponent } from './search-flights/search-flights.component';

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideRouter([
      { path: '', component: SearchFlightsComponent, pathMatch: 'full' },
    ]),
  ],
};
