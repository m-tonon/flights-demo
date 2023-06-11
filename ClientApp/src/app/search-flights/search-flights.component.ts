import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { FlightRm } from 'src/api/models';
import { TimePlaceRm } from 'src/api/models';
import { FlightService } from 'src/api/services';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule, NavMenuComponent],
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css'],
})
export class SearchFlightsComponent {
  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService) {}

  search() {
    this.flightService.searchFlight({}).subscribe({
      next: (response: any) => (this.searchResult = response),
      error: this.handleError,
    });
  }

  private handleError(error: any) {
    console.error(error);
  }
}
