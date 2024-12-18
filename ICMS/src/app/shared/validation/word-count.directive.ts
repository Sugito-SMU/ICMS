import { ValidatorFn, AbstractControl } from '@angular/forms';
import { SYSTEM_CONSTANTS } from '../system.constants';

export function wordCountValidator(min: number, max: number, group: string, initial: boolean = null): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
        let IsSelected = null;

        try {
            IsSelected = JSON.parse(control.parent.parent.parent.get(group).value);
        }
        catch
        {
            IsSelected = initial;
        }

        if (IsSelected == (group == 'IsSelected' ? true : false) || (group != "IsSelected" && IsSelected == null)) {
            if (control.value) {
                var matches = control.value.match(SYSTEM_CONSTANTS.WC_REGEX);
                if (matches == null || matches.length > max || matches.length < min) {
                    return { 'wordCount': true };
                }
            }
            else {
                return { 'wordCount': true };
            }
        }
        return null;
    };
}

export function wordCountPostValidator(min: number, max: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
        if (control.value) {
            var matches = control.value.match(SYSTEM_CONSTANTS.WC_REGEX);

            if (matches == null || matches.length > max || matches.length < min) {
                return { 'wordCount': true };
            }
        }
        else {
            return { 'wordCount': true };
        }
        return null;
    };
}

export function wordCountOnChange(min: number, max: number, group: string): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
        let questions = null;
        let error = null;
        try {
            questions = control.parent.controls[group];
        }
        catch {

        }

        if (questions && JSON.parse(control.value) == (group == 'LearningObjectiveQuestions' ? true : false)) {
            questions.controls.forEach((value, index) => {
                if (value.controls['Answer'].value != null) {
                    var matches = value.controls['Answer'].value.match(SYSTEM_CONSTANTS.WC_REGEX);

                    if (matches == null || matches.length > max || matches.length < min) {
                        value.controls['Answer'].updateValueAndValidity();
                    }
                }
                else {
                    value.controls['Answer'].updateValueAndValidity();
                }
            });
        }
        else if (questions) {
            questions.controls.forEach((value, index) => {
                value.controls['Answer'].updateValueAndValidity();
            });
        }
        return error;
    };
}

export function leanringObjectiveValidation(): ValidatorFn | any {
    return (control: AbstractControl) => {
        let lo: any;
        let count = 0;

        lo = control.value;
        
        lo.forEach((value, index) => {
            if (value.IsSelected) {
                count++;
            }
        });

        if (count < 3) {
            return { 'lo_count': true };
        }

        return null;
    }
}