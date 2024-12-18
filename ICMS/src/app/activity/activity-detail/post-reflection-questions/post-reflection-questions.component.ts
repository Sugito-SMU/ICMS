import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivityQuestionAnswer } from '../learning-objective';
import { ActivityTypes, VALIDATION_PARAMS, SYSTEM_CONSTANTS } from '../../../shared/system.constants';
import { FormGroup, FormGroupDirective, FormControl, ControlContainer, FormArray } from '@angular/forms';
import { wordCountPostValidator } from 'src/app/shared/validation/word-count.directive';
import { trigger, transition, animate, style } from '@angular/animations';

@Component({
  selector: 'app-post-reflection-questions',
  templateUrl: './post-reflection-questions.component.html',
    styleUrls: ['./post-reflection-questions.component.css'],
    viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }],
    animations: [
        trigger('slideInOut', [
            transition(':enter', [
                style({ transform: 'translateY(-50%)' }),
                animate('200ms ease-in', style({ transform: 'translateY(0%)' }))
            ]),
            transition(':leave', [
                animate('200ms ease-in', style({ transform: 'translateY(-50%)' }))
            ])
        ])
    ]
})
export class PostReflectionQuestionsComponent implements OnInit {
    @Input() activitytype: string;
    @Input() activityquestions: ActivityQuestionAnswer[];
    @Input() header: string;
    @Input() allDisabled: boolean;
    @Input() readonly showValidation: boolean;

    @Output() onLoad: EventEmitter<any> = new EventEmitter<any>();
    childForm;

    readonly ActivityTypes = ActivityTypes;
    readonly VALIDATION_PARAMS = VALIDATION_PARAMS;

    constructor(private parentF: FormGroupDirective) { }

    ngOnInit() {
        this.toFormGroup();
    }

    toFormGroup() {
        let group: any = [];
        this.childForm = this.parentF.form;
        this.activityquestions.forEach(aq => {
            group.push(new FormGroup({ ActivityId: new FormControl(aq.ActivityId), ActivityQuestionId: new FormControl(aq.ActivityQuestionId), Answer: new FormControl(aq.Answer, [wordCountPostValidator(VALIDATION_PARAMS.TA_MIN_WC_POSTA, VALIDATION_PARAMS.TA_MAX_WC_POSTA)]) }))
        });
        this.childForm.addControl('ActivityQuestionAnswers', new FormArray(group))
    }

    getAnswer(index: number) {
        return this.childForm.controls['ActivityQuestionAnswers'].controls[index].controls['Answer'];
    }

    getWordCount(value: string) {
        if (value != null && value != "") {
            return value.match(SYSTEM_CONSTANTS.WC_REGEX).length;
        }
        else {
            return 0;
        }
    }

    contentUpdated() {
        this.onLoad.emit();
    }

    toggle(index: number) {
        this.activityquestions[index].Show = !this.activityquestions[index].Show;
    }

    tooltipText(index: number) {
        if (this.activityquestions[index].Show) {
            return "Hide Details";
        }
        else {
            return "Show Details";
        }
    }
}