import { Injectable } from '@angular/core';
import { Area } from './services/api';

@Injectable({
  providedIn: 'root',
})
export class DummyDataService {
  private dummyAreas: Area[] = [];

  constructor() {
    this.dummyAreas = this.generateDummyAreas(15);
  }

  // Generate dummy areas
  generateDummyAreas(count: number): Area[] {
    const dummyAreas: Area[] = [];

    for (let i = 1; i <= count; i++) {
      const dummyArea: Area = {
        areaId: `Area${i}`,
        name: `Area ${i}`,
        trucks: [],
        locations: [],
        init: function (_data?: any): void {
          throw new Error('Function not implemented.');
        },
        toJSON: function (data?: any) {
          throw new Error('Function not implemented.');
        },
      };

      dummyAreas.push(dummyArea);
    }

    return dummyAreas;
  }

  getDummyAreas(): Area[] {
    return this.dummyAreas;
  }

  getAreaById(areaId: string): Area | undefined {
    return this.dummyAreas.find((area) => area.areaId === areaId);
  }
}