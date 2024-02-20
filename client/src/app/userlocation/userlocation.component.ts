import { Component } from '@angular/core';

@Component({
  selector: 'app-userlocation',
  templateUrl: './userlocation.component.html',
  styleUrl: './userlocation.component.css'
})
export class UserlocationComponent {
  locations = [
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    {LocationID: 1, type: "String", AreaID: 1 },
    // 假设这些数据来自后端
  ];

  constructor() { }

  ngOnInit(): void {
  }

  createView() {
    // 实现查看逻辑
    console.log('Viewing location...');
  }

  cancel() {
    // 实现取消逻辑
    console.log('Operation cancelled.');
  }

}
