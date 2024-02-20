import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminedit',
  templateUrl: './adminedit.component.html',
  styleUrl: './adminedit.component.css'
})
export class AdmineditComponent {

  editingLocation = {LocationID: 1, Type: "String", AreaID: 1 }

  constructor(private router: Router) {}

  cancel() {
    this.router.navigate(['/adminlocation']);
  }
  
  confirm() {
    // 这里添加更新或创建 location 的逻辑
    console.log(this.editingLocation); // 示例逻辑
    this.router.navigate(['/admin-location']);
  }
  
}
