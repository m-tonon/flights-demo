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
  _email: any = this.form.get('email')?.value;


  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService
  ) {}

  ngOnInit() {}

  checkPassenger(): void {
    this.passengerService.findPassenger(this._email)
      .subscribe(_ => {
        this.login();
      })
  }

  register() {
    console.log(this.form.value)

    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe({
        next: res => this.login(),
        error: err => console.error()
      })
  }

  private login() {
    this.authService.loginUser({ email: this._email });
  }
}
