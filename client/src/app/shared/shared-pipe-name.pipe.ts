import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sharedPipeName',
  standalone: true
})
export class SharedPipeNamePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
