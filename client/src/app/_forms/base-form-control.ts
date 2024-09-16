import { FormControl, NgControl } from '@angular/forms';

export abstract class BaseFormControl {
  constructor(protected ngControl: NgControl) {}

  // This getter method retrieves the FormControl instance associated with the ngControl directive.
  // By casting ngControl.control to FormControl, it ensures TypeScript recognizes the type correctly,
  // preventing type errors in the template when accessing control properties and methods.
  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}
