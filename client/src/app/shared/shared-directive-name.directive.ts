import { Directive } from '@angular/core';

@Directive({
  selector: '[appSharedDirectiveName]',
  standalone: true
})
export class SharedDirectiveNameDirective {

  constructor() { }

}
