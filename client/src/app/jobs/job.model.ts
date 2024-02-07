// job.model.ts
export interface Job {
  id: number;
  location: string;
  description: string;
  status: 'pending' | 'in-progress' | 'completed';
}
