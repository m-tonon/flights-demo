import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookingRm } from 'src/api/models/booking-rm';
import { BookingService } from 'src/api/services';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { BookDto } from 'src/api/models/book-dto';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css'],
})
export class MyBookingsComponent implements OnInit {
  bookings!: BookingRm[];

  constructor(
    private bookingService: BookingService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.authService.currentUser?.email) {
      this.router.navigate(['/register-passenger']);
    }

    const _email = this.authService.currentUser?.email ?? '';

    this.bookingService.listBooking({ email: _email }).subscribe({
      next: (r) => (this.bookings = r),
      error: this.handleError,
    });
  }

  private handleError(error: any) {
    console.log('Response error, Status: ', error.status);
    console.log('Response error, Status Text: ', error.statusText);
    console.log(error);
  }

  cancel(booking: BookingRm) {
    
    const dto: BookDto = {
      flightId: booking.id,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail
    };

    this.bookingService.cancelBooking({ body: dto}).subscribe({
      next: _ => this.bookings = this.bookings.filter(b => b.id !== booking.id),
      error: this.handleError
    });
  }
}
