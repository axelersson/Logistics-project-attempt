import { Injectable } from '@angular/core';
import { Area } from './services/api'; // Correct import path

@Injectable({
  providedIn: 'root',
})
export class DummyDataService {
  constructor() {}

  // Generate dummy areas
  generateDummyAreas(count: number): Area[] {
    const dummyAreas: Area[] = [];

    for (let i = 1; i <= count; i++) {
      const dummyArea: Area = {
        areaId: `Area${i}`,
        name: `Area ${i}`,
        //locationIds: [`Location${i}_1`, `Location${i}_2`],
        trucks: [],
        locations: [],
        init: function (_data?: any): void {
          throw new Error('Function not implemented.');
        },
        toJSON: function (data?: any) {
          throw new Error('Function not implemented.');
        }
      };

      dummyAreas.push(dummyArea);
    }

    return dummyAreas;
  }
}
