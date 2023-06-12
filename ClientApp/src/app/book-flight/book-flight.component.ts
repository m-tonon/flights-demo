import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css'],
})
export class BookFlightComponent implements OnInit {
  flightId: string = 'not loaded';
  
  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap
      .subscribe(params => this.flightId = params.get('flightId') ?? 'not provided');
  }
}
