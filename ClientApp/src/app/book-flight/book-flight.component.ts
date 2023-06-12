import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {
  
    constructor() { }
  
    ngOnInit() {
    }
}
