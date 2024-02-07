import { Injectable } from '@angular/core';

export interface Job {
  id: number;
  location: string;
  description: string;
  status: 'pending' | 'in-progress' | 'completed';
}

@Injectable({
  providedIn: 'root',
})
export class MockDataService {
  private jobs: Job[] = [
    {
      id: 1,
      location: 'Machine 1',
      description: 'Deliver 5 steel rolls',
      status: 'pending',
    },
    {
      id: 2,
      location: 'Storage Area A',
      description: 'Pick up 3 steel rolls',
      status: 'in-progress',
    },
    {
      id: 3,
      location: 'Machine 3',
      description: 'Deliver 2 steel rolls',
      status: 'completed',
    },
  ];

  constructor() {}

  getJobs(): Job[] {
    return this.jobs;
  }
}
