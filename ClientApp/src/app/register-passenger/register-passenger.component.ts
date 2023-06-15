import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

import { PassengerService } from 'src/api/services';
import { AuthService } from '../services/auth.service';

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
    private authService: AuthService
  ) {}

  ngOnInit() {}

  checkPassenger(): void {
    const params = {email: this.form.get('email')?.value};

    this.passengerService.findPassenger(params)
      .subscribe(_ => {
        console.log('Passenger exists. Loggin in now');
        this.authService.loginUser({ email: params.email });
      })
  }

  register() {
    console.log(this.form.value)

    const email = this.form.get('email')?.value;

    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe({
        next: res => this.authService.loginUser({ email: email}),
        error: err => console.error()
      })
  }
}
