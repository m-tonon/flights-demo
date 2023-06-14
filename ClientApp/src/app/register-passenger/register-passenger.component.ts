import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PassengerService } from 'src/api/services';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

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
    private fb: FormBuilder
  ) {}

  ngOnInit() {}

  register() {
    console.log(this.form.value)

    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(res => console.log('form posted to server'))
  }
}
