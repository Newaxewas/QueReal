import { AbstractControl, ValidatorFn } from "@angular/forms";

export class CustomValidators {
  static matchValue(matchToElement: AbstractControl) : ValidatorFn {
    const validator = (element: AbstractControl) => {
      if (element.value !== matchToElement.value) {
        return { matchValue: true };
      } else {
        return null;
      }
    }

    return validator;
  }
}
