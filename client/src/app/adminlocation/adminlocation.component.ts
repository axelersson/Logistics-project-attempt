import { Component } from '@angular/core';

@Component({
  selector: 'app-adminlocation',
  templateUrl: './adminlocation.component.html',
  styleUrl: './adminlocation.component.css'
})
export class AdminlocationComponent {
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

  deleteLocation() {
    // 实现删除逻辑
    console.log('Deleting location...');
  }

  createLocation() {
    // 实现创建逻辑
    console.log('Creating new location...');
  }

  editLocation() {
    // 实现编辑逻辑
    console.log('Editing location...');
  }

  cancel() {
    // 实现取消逻辑
    console.log('Operation cancelled.');
  }

}
