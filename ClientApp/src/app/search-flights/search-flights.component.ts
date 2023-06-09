import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  reaminingSeats: number;
  price: string;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [
    CommonModule,
    NavMenuComponent
  ],
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {
  
  searchResult: FlightRm[] = [
    {
      airline: "American Airlines",
      departure: { place: "Los Angeles", time: Date.now().toString() },
      arrival: { place: "Istambul", time: Date.now().toString() },
      reaminingSeats: 500,
      price: "500"
    },
    {
      airline: "Deutsche BA",
      departure: { place: "Munchen", time: Date.now().toString() },
      arrival: { place: "Schiphol", time: Date.now().toString() },
      reaminingSeats: 60,
      price: "600"
    },
    {
      airline: "British Airways",
      departure: { place: "London, England", time: Date.now().toString() },
      arrival: { place: "Vizzola-Ticino", time: Date.now().toString() },
      reaminingSeats: 60,
      price: "600"
    },
  ];
}
