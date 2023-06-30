import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { FlightRm } from 'src/api/models';
import { TimePlaceRm } from 'src/api/models';
import { FlightService } from 'src/api/services';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule ,NavMenuComponent],
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css'],
})
export class SearchFlightsComponent implements OnInit {
  searchResult: FlightRm[] = [];
  searchForm = this.fb.group({
    fromDate: [''],
    toDate: [''],
    from: [''],
    destination: [''],
    numberOfPassengers: [1]
  });

  constructor(
    private flightService: FlightService,
    private fb: FormBuilder) {}

  ngOnInit(): void {
    this.search();
  }

  search() {
    this.flightService.searchFlight(this.searchForm.value).subscribe({
      next: (response: any) => (this.searchResult = response),
      error: this.handleError,
    });
  }

  private handleError(error: any) {
    console.log("Response Error. Status", error.status);
    console.log("Response Error. Status Text", error.statusText);
    console.log(error);
  }
}
