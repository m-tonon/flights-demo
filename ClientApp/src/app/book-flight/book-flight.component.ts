import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import { FlightService } from 'src/api/services';
import { BookDto, FlightRm } from 'src/api/models';
import { AuthService } from '../services/auth.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css'],
})
export class BookFlightComponent implements OnInit {
  flightId: string = 'not loaded';
  flight: FlightRm = {};
  form = this.fb.group({
    number: [1],
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    if (!this.authService.currentUser)
      this.router.navigate(['/register-passenger']);

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
  };

  book() {
    console.log(
      `Booking ${this.form.get('number')?.value} passenger for flight ${
        this.flightId
      }`
    );

    const booking: BookDto = {
      flightId: this.flight.id,
      numberOfSeats: this.form.get('number')?.value,
      passengerEmail: this.authService.currentUser?.email,
    };

    this.flightService.bookFlight({ body: booking }).subscribe({
      next: (_) => console.log('succeded'),
      error: this.handleError,
    });
  }
}
