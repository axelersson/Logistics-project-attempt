// In mock-data.service.ts
import { Job } from './jobs/job.model'; // Adjust the import path as necessary
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MockDataService {
  private jobs: Job[] = [
    // your jobs array
  ];

  getJobs(): Job[] {
    return this.jobs;
  }
}
