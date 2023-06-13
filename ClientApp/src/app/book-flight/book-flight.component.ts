import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import { FlightService } from 'src/api/services';
import { FlightRm } from 'src/api/models';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css'],
})
export class BookFlightComponent implements OnInit {
  flightId: string = 'not loaded';
  flight: FlightRm = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) =>
      this.findFlight(params.get('flightId'))
    );
  }

  private findFlight = (_flightId: string | null) => {
    this.flightId = _flightId ?? 'not provided';

    this.flightService.findFlight({ id: this.flightId }).subscribe({
      next: (_flight) => (this.flight = _flight),
      error: this.handleError,
    });
  };

  private handleError = (error: any) => {
    if (error.status === 404) {
      alert('Flight not found!');
      this.router.navigate(['/search-flights']);
    }

    console.log('Response Error. Status', error.status);
    console.log('Response Error. Status Text', error.statusText);
    console.log(error);
  }
}
