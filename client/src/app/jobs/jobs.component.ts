import { Component, OnInit } from '@angular/core';
import { MockDataService } from '../mock-data.service';
import { Job } from './job.model';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css'],
})
export class JobsComponent implements OnInit {
  jobs: Job[] = []; // Explicitly type the array as Job[]

  constructor(private mockDataService: MockDataService) {}

  ngOnInit(): void {
    this.jobs = this.mockDataService.getJobs();
  }
}
