import { Component, OnInit } from '@angular/core';
import { DummyDataService } from '../dummy-data.service';
import { Area } from '../services/api'; // Correct import path

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css'], // Correct property name
})
export class AreaComponent implements OnInit {
  areas: Area[] = [];

  constructor(private dummyDataService: DummyDataService) {}

  ngOnInit(): void {
    // Generate dummy areas (you can specify the count you need)
    this.areas = this.dummyDataService.generateDummyAreas(15);
  }
}