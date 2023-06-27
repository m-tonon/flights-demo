import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { PassengerService } from 'src/api/services';
import { AuthService } from '../auth/auth.service';
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
    email: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
        Validators.email,
      ]),
    ],
    firstName: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(35),
      ]),
    ],
    lastName: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(35),
      ]),
    ],
    isFemale: [true, Validators.required],
  });

  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {}

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value };
    console.log(params);

    this.passengerService.findPassenger(params).subscribe({
      next: (res) => this.login(),
      error: (err) => {
        if (err.status != 404) console.error(err);
      },
    });
  }

  register() {
    if (this.form.invalid) 
      return;

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
