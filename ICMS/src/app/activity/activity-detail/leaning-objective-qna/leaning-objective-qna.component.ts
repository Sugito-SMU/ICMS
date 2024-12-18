import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { QuestionAnswer, SequenceContent } from '../learning-objective';
import { trigger, transition, animate, style } from '@angular/animations';
import { ActivityStatusTypes, SYSTEM_CONSTANTS, VALIDATION_PARAMS } from 'src/app/shared/system.constants';
import { FormGroup, FormGroupDirective, FormControl, Validators, ControlContainer, FormArray } from '@angular/forms';
import { wordCountValidator } from '../../../shared/validation/word-count.directive';

@Component({
    selector: 'app-leaning-objective-qna',
    templateUrl: './leaning-objective-qna.component.html',
    styleUrls: ['./leaning-objective-qna.component.css'],
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
    ],
    viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class LeaningObjectiveQnaComponent implements OnInit {
    @Input() indicators: SequenceContent[];
    @Input() questionanswers: QuestionAnswer[];
    @Input() stage: string;
    @Input() MidActivityNotOnTargetInstruction: string;
    @Input() stagequestionanswers: QuestionAnswer[];
    @Input() yesno: boolean;
    @Input() index: number;
    @Input() LearningObjectiveId: string;
    @Input() allDisabled: boolean;
    @Input() readonly showValidation: boolean;
    questionGroup;
    childForm;

    @Output() achievedChange: EventEmitter<any> = new EventEmitter<any>();
    @Output() ontrackChange: EventEmitter<any> = new EventEmitter<any>();
    @Output() answersEmpty: EventEmitter<any> = new EventEmitter<any>();

    @Output() onLoad: EventEmitter<any> = new EventEmitter<any>();

    readonly ActivityStatusTypes = ActivityStatusTypes;

    public question: string;
    yesnoquestion: string;
    readonly VALIDATION_PARAMS = VALIDATION_PARAMS;

    constructor(private parentF: FormGroupDirective) { }

    ngOnInit() {
        
        if (this.stage == ActivityStatusTypes.PRE.Code) {
            this.yesnoquestion = 'IsSelected';
            this.questionGroup = 'LearningObjectiveQuestions';
        }
        else if (this.stage == ActivityStatusTypes.MID.Code) {
            this.question = "Are you on target with this learning objective?";
            this.yesnoquestion = 'IsMidActivityOnTarget';
            this.questionGroup = 'LearningObjectiveStageQuestionAnswers';
        }
        else if (this.stage == ActivityStatusTypes.POST.Code) {
            this.question = "Did you achieve your learning objective?";
            this.yesnoquestion = 'IsObjectiveAchieved';
            this.questionGroup = 'LearningObjectiveStageQuestionAnswers';
        }

        this.toFormGroup();
        
        //For showing red underline initially
        this.contentUpdated(true);
    }

    toFormGroup() {
        let group: any = [];
        this.childForm = this.parentF.form;
        switch (this.stage) {
            case ActivityStatusTypes.PRE.Code:
                this.questionanswers.forEach(loq => {
                    group.push(new FormGroup({ ActivityId: new FormControl(loq.ActivityId), LearningObjectiveId: new FormControl(loq.LearningObjectiveId), LearningObjectiveQuestionId: new FormControl(loq.LearningObjectiveQuestionId), Answer: new FormControl(loq.Answer, [wordCountValidator(VALIDATION_PARAMS.TA_MIN_WC, VALIDATION_PARAMS.TA_MAX_WC, this.yesnoquestion, this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.yesnoquestion].value)]) }))
                });
                break;
            case ActivityStatusTypes.MID.Code:
            case ActivityStatusTypes.POST.Code:
                this.stagequestionanswers.forEach(loq => {
                    group.push(new FormGroup({ ActivityId: new FormControl(loq.ActivityId), LearningObjectiveId: new FormControl(loq.LearningObjectiveId), LearningObjectiveQuestionId: new FormControl(loq.LearningObjectiveQuestionId), Answer: new FormControl(loq.Answer, [wordCountValidator(VALIDATION_PARAMS.TA_MIN_WC, VALIDATION_PARAMS.TA_MAX_WC, this.yesnoquestion, this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.yesnoquestion].value)]) }))
                });
                break;
        }
        this.childForm.controls['LearningObjectives'].controls[this.index].addControl(this.questionGroup, new FormArray(group));
    }

    yesnoToggle(event: any, status: boolean) {
        debugger;
        this.yesno = status;
        
        this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.questionGroup].patchValue([{ Answer: "" }]);

        this.contentUpdated();
    }

    contentUpdated(firstTime: boolean = false) {
        if (!firstTime) {
            this.onLoad.emit();
        }
        let status: boolean = false;

        if ((this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.yesnoquestion].value == "true"
            && this.stage != ActivityStatusTypes.PRE.Code) ||
            this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.questionGroup]
            .controls.filter(c => c.controls['Answer'].value == "" || c.controls['Answer'].value == null || this.getWordCount(c.controls['Answer'].value) > VALIDATION_PARAMS.TA_MAX_WC).length == 0) {
            status = this.stage != ActivityStatusTypes.PRE.Code;
        }
        else {
            status = this.stage == ActivityStatusTypes.PRE.Code;
        }

        switch (this.stage) {
            case ActivityStatusTypes.PRE.Code:
                this.answersEmpty.emit(status);
                break;
            case ActivityStatusTypes.MID.Code:
                this.ontrackChange.emit(status);
                break;
            case ActivityStatusTypes.POST.Code:
                this.achievedChange.emit(status);
                break;
        }
    }

    getAnswer(index: number) {
        return this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.questionGroup].controls[index].controls['Answer'];
    }

    getyesno() {
        return this.childForm.controls['LearningObjectives'].controls[this.index].controls[this.yesnoquestion];
    }

    getWordCount(value: string) {
        if (value != null && value != "") {
            return value.match(SYSTEM_CONSTANTS.WC_REGEX).length;
        }
        else {
            return 0;
        }
    }
}
