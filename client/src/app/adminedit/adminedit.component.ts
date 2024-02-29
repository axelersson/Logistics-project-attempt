import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LocationService } from '../services/location.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-adminedit',
  templateUrl: './adminedit.component.html',
  styleUrls: ['./adminedit.component.css']
})
export class AdmineditComponent {
  isEditing = false;
  editingLocation = { locationId: '', areaId: '', locationType: 'Machine' };
  locationidThatWantToEdit = ""
  
  constructor(
    private locationService: LocationService,
    private router: Router,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    const locationId = this.route.snapshot.paramMap.get('locationId'); // 从路由中获取locationId
    if (locationId !== null) {
      this.locationidThatWantToEdit = locationId;
    }
    if (locationId) {
      this.editingLocation.locationId = locationId;
      this.isEditing = true; // 设置为true表示编辑现有位置
      // 这里你也可以添加逻辑来加载该位置的其他信息，如areaId和locationType
    }
  }

  confirm(): void {
    // 首先检查输入是否为空
    if (!this.editingLocation.locationId || !this.editingLocation.areaId) {
        this.snackBar.open('Please complete all fields', 'Close', { duration: 3000 });
        return;
    }
    // 检查是否处于编辑模式
    if (this.isEditing) {
        // 更新现有位置
        this.locationService.updateLocation(this.editingLocation.locationId, this.editingLocation).subscribe({
            next: () => {
                this.snackBar.open('Location updated successfully', 'Close', { duration: 3000 });
                this.router.navigate(['/adminlocation']);
            },
            error: (err) => {
                // 显示从服务返回的错误消息
                // 注意：后端需要正确处理并返回错误消息
                const errorMsg = err.error.message || 'Failed to update location';
                this.snackBar.open(errorMsg, 'Close', { duration: 3000 });
            }
        });
    } else {
        // 创建新位置
        this.locationService.createLocation(this.editingLocation).subscribe({
            next: (location) => {
                this.snackBar.open('Location created successfully', 'Close', { duration: 3000 });
                this.router.navigate(['/adminlocation']);
            },
            error: (err) => {
                // 显示从服务返回的错误消息
                const errorMsg = err.message || 'Failed to create location';
                this.snackBar.open(errorMsg, 'Close', { duration: 3000 });
            }
        });
    }
}


  cancel(): void {
    this.router.navigate(['/adminlocation']); 
  }
}
