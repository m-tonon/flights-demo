import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [
    CommonModule,
    NavMenuComponent
  ],
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

}
