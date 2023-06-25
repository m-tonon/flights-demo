import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookingRm } from 'src/api/models/booking-rm';
import { BookingService } from 'src/api/services';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

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
}
