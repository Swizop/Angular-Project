import { Directive } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function forbiddenPhoneValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    var r = new RegExp (/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im);

    const good = r.test(control.value);
    return good ? null : {forbiddenPhone: {value: control.value}}
  };
}

