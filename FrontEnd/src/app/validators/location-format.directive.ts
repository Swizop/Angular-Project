
 import { Directive } from '@angular/core';
 import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';


 export function locationValidator(): ValidatorFn {
   return (control: AbstractControl): ValidationErrors | null => {
     var r = new RegExp (/^[a-zA-Z]{1,20}, [a-zA-Z]{1,20}$/);

     const good = r.test(control.value);
     return good ? null : {location: {value: control.value}}
   };
 }