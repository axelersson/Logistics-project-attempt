import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sortByStatus'
})
export class SortByStatusPipe implements PipeTransform {
  transform(orders: any[]): any[] {
    if (!orders) {
      return [];
    }
    const statusPriority = ['Pending', 'PartiallyDelivered', 'Delivered', 'Cancelled'];
    return orders.sort((a, b) => statusPriority.indexOf(a.orderStatus) - statusPriority.indexOf(b.orderStatus));
  }
}
