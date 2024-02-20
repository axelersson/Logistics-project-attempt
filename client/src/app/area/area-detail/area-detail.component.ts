import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DummyDataService } from '../../dummy-data.service';
import { Area } from '../../services/api';

@Component({
  selector: 'app-area-detail',
  templateUrl: './area-detail.component.html',
  styleUrls: ['./area-detail.component.css'],
})
export class AreaDetailsComponent implements OnInit {
  @Input() area: Area | undefined;

  constructor(
    private route: ActivatedRoute,
    private dummyDataService: DummyDataService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const areaId = params.get('areaId');
      if (areaId) {
        // Fetch area details using areaId from stored dummy data
        this.area = this.dummyDataService.getAreaById(areaId);
      }
    });
  }
}