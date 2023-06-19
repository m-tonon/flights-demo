import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

import { PassengerService } from 'src/api/services';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css'],
})
export class RegisterPassengerComponent implements OnInit {
  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true],
  });

  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {}

  checkPassenger(): void {
    const params = {email: this.form.get('email')?.value};
    console.log(params);

    this.passengerService.findPassenger(params).subscribe({
      next: (res) => this.login(),
      error: (err) => {
        if(err.status != 404)
        console.error(err)
      }
    });
  }

  register() {
    console.log(this.form.value);

    this.passengerService
      .registerPassenger({ body: this.form.value })
      .subscribe({
        next: (res) => this.login(),
        error: (err) => console.error(),
      });
  }

  private login() {
    const _email = this.form.get('email')?.value;
    this.authService.loginUser({ email: _email });
    this.router.navigate(['/search-flights']);
  }
}
