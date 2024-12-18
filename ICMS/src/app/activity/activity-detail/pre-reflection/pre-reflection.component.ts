import { Component, OnInit, ViewChild, Input, ViewChildren, QueryList, EventEmitter, Output } from '@angular/core';
import { CheckBoxComponent } from '@syncfusion/ej2-angular-buttons';
import { enableRipple, EmitType } from '@syncfusion/ej2-base';
import { LearningObjective, ActivityQuestionAnswer } from '../learning-objective';
import { AccordionComponent } from '@syncfusion/ej2-angular-navigations';
import { ActivityStatusTypes, SYSTEM_CONSTANTS, LocalStorageKeys, VALIDATION_PARAMS } from 'src/app/shared/system.constants';
import { ActivityService } from 'src/app/services/activity.service';
import { FormControl, FormGroup, FormArray } from '@angular/forms';
import { LeaningObjectiveQnaComponent } from '../leaning-objective-qna/leaning-objective-qna.component';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { wordCountOnChange, leanringObjectiveValidation } from 'src/app/shared/validation/word-count.directive';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';
import { Helper } from 'src/app/shared/helpers/icms.helper';
import { DialogComponent } from '@syncfusion/ej2-angular-popups';

enableRipple(true);

@Component({
    selector: 'app-pre-reflection',
    templateUrl: './pre-reflection.component.html',
    styleUrls: ['./pre-reflection.component.css']
})
export class PreReflectionComponent implements OnInit {

    @Input() readonly stage: string;
    @Input() readonly activitytype: string;
    @Input() readonly activityid: string;
    @Input() readonly showValidation: boolean;

    @ViewChild('accordion') accordion: AccordionComponent;
    @ViewChildren('loq') loqs: QueryList<LeaningObjectiveQnaComponent>;
    @ViewChildren('checkbox') checkboxes: QueryList<CheckBoxComponent>;

    @ViewChild('validationDialog')
    public validationDialog: DialogComponent;
    @ViewChild('loChangeDialog')
    public loChangeDialog: DialogComponent;

    @Output() valueChanged: EventEmitter<any> = new EventEmitter<any>();

    readonly ActivityStatusTypes = ActivityStatusTypes;

    public checkbox: CheckBoxComponent;
    public disable: false;
    public status: boolean;
    public learningobjectives: LearningObjective[];
    public activityquestions: ActivityQuestionAnswer[];
    public form: FormGroup = new FormGroup({});
    finished: boolean = false;
    load: boolean = false;
    questionGroup = this.stage == ActivityStatusTypes.PRE.Code ? 'LearningObjectiveQuestions' : 'LearningObjectiveStageQuestionAnswers';
    postReflectionHeader: string = "";
    inProgress: boolean = false;
    manualSave: boolean = false;
    public allDisabled: boolean = true;
    timeoutSet: boolean = false;
    isAdmin: boolean = true;
    student_id = null;
    validationMessage = "";

    public loChangeHeader: string = 'Please confirm';
    public loChangeMessage: string = 'You are going to change your learning objective selection. The existing responses will be removed. Please confirm.';

    public validationHeader: string = 'Incomplete';
    public confirmHeader: string = 'Are you sure?';
    public confirmWidth: string = '400px';
    public confirmCloseIcon: Boolean = true;
    public hidden: Boolean = false;
    public isModal: Boolean = true;
    public animationSettings: Object = { effect: 'None' };
    isChrome: boolean = true;

    loChangeEvent;
    loChangeIndex;

    constructor(private route: ActivatedRoute, private activityService: ActivityService, private router: Router, private toastr: ToastrService,
        private authenticationService: AuthenticationService, public sharedService: SharedService) { }

    ngOnInit() {
        this.authenticationService.getAuthToken().subscribe(response => {
            if (response) {
                this.sharedService.jsonData = response;

                this.getLeanringObjectives();

                if (this.stage == ActivityStatusTypes.POST.Code) {
                    this.getActivityQuestions();
                    this.getPostReflectionHeader();
                }
            }
        });
        this.valueChanged.emit(false);

        this.isAdmin = localStorage.getItem(LocalStorageKeys.IsAdmin) == 'True';
        this.student_id = localStorage.getItem(LocalStorageKeys.StudentId);

        if (this.isAdmin) {
            this.student_id = this.route.snapshot.paramMap.get('studentId');
        }

        if (this.stage == ActivityStatusTypes.PRE.Code) {
            this.questionGroup = 'LearningObjectiveQuestions';
            this.finished = true;
            this.allDisabled = this.isAdmin || localStorage.getItem(LocalStorageKeys.EnablePreActivity) == 'true';
        }
        else if (this.stage == ActivityStatusTypes.MID.Code) {
            this.questionGroup = 'LearningObjectiveStageQuestionAnswers';
            this.finished = true;
            this.allDisabled = this.isAdmin || localStorage.getItem(LocalStorageKeys.EnableMidActivity) == 'true';
        }
        else if (this.stage == ActivityStatusTypes.POST.Code) {
            this.questionGroup = 'LearningObjectiveStageQuestionAnswers';
            this.allDisabled = this.isAdmin || localStorage.getItem(LocalStorageKeys.EnablePostActivity) == 'true';
        }

        this.isChrome = /chrome\//i.test(window.navigator.userAgent)
    }

    getLeanringObjectives() {
        this.activityService.getLearningObjectives(this.activityid, this.stage, this.student_id).subscribe(learningobjectives => {
            this.learningobjectives = learningobjectives.result;

            this.sortLearningObjectives();

            this.createFormGroup();

            if (this.stage == ActivityStatusTypes.PRE.Code) {
                //timeout is needed because we have to enable/ disable accordion items after it is rendered
                setTimeout(() => { this.setAccordianItemState(this) }, 500);
            }

            setTimeout(() => { this.ExpandAccordian() }, 500);
        });
    }

    getActivityQuestions() {
        this.activityService.getActivityQuestions(this.activityid, this.student_id).subscribe(activityquestions => {
            this.createFormGroup();
            this.activityquestions = activityquestions.result;
        });
    }

    getPostReflectionHeader() {
        this.activityService.getPostReflectionHeader(this.activityid).subscribe(postReflectionHeader => {
            this.postReflectionHeader = postReflectionHeader;
        });
    }

    createFormGroup() {
        if (this.finished) {
            this.toFormGroup();
        }
        else {
            this.finished = true;
        }
    }

    toFormGroup() {
        let group: any = [];

        this.form.addControl('ActivityId', new FormControl(this.activityid));
        this.form.addControl('Stage', new FormControl(this.stage));
        this.form.addControl('Submit', new FormControl(false));

        switch (this.stage) {
            case ActivityStatusTypes.PRE.Code:
                this.learningobjectives.forEach(lo =>
                    group.push(new FormGroup({ ActivityId: new FormControl(this.activityid), LearningObjectiveId: new FormControl(lo.LearningObjectiveId), IsSelected: new FormControl(lo.IsSelected, wordCountOnChange(VALIDATION_PARAMS.TA_MIN_WC, VALIDATION_PARAMS.TA_MAX_WC, this.questionGroup)) }))
                );

                this.form.addControl('LearningObjectives', new FormArray(group, leanringObjectiveValidation()));
                break;
            case ActivityStatusTypes.MID.Code:
                this.learningobjectives.forEach(lo =>
                    group.push(new FormGroup({ ActivityId: new FormControl(this.activityid), LearningObjectiveId: new FormControl(lo.LearningObjectiveId), IsMidActivityOnTarget: new FormControl(String(lo.IsMidActivityOnTarget), wordCountOnChange(VALIDATION_PARAMS.TA_MIN_WC, VALIDATION_PARAMS.TA_MAX_WC, this.questionGroup)) }))
                );

                this.form.addControl('LearningObjectives', new FormArray(group));
                break;
            case ActivityStatusTypes.POST.Code:
                this.learningobjectives.forEach(lo =>
                    group.push(new FormGroup({ ActivityId: new FormControl(this.activityid), LearningObjectiveId: new FormControl(lo.LearningObjectiveId), IsObjectiveAchieved: new FormControl(String(lo.IsObjectiveAchieved), wordCountOnChange(VALIDATION_PARAMS.TA_MIN_WC, VALIDATION_PARAMS.TA_MAX_WC, this.questionGroup)) }))
                );

                this.form.addControl('LearningObjectives', new FormArray(group));
                break;
        }
        this.load = true;
    }


    public onSubmit(submit: boolean = false) {
        this.inProgress = true;
        let updatedLearningObjectives: any;

        if (this.showValidationMessage(submit)) {
            return;
        }
        
        this.form.controls["Submit"].setValue(submit);

        updatedLearningObjectives = Helper.getActivityJSONString(this.form.value);

        this.authenticationService.getAuthToken().subscribe(response => {
            if (response) {
                this.sharedService.jsonData = response;
            }

            this.activityService.updateActivity(updatedLearningObjectives)
                .subscribe((result) => {
                    if (result) {
                        this.valueChanged.emit(false);
                        this.inProgress = false;
                        this.manualSave = true;

                        let tabToDisplay = localStorage.getItem(LocalStorageKeys.RefreshDisplayTab);
                        if (tabToDisplay || submit) {
                            localStorage.setItem(LocalStorageKeys.RefreshToastMessage, "Reflection has been " + (submit ? "submitted" : "saved") + " successfully");
                            window.location.reload();
                        }
                        else {
                            this.toastr.success('', 'Reflection has been saved successfully.');
                        }
                    }
                    else if (result == undefined) {
                        this.toastr.error('', 'System currently encounters an error. Please try again.');
                    }
                    else {
                        this.toastr.error('', 'You are not allowed to change after submission.');
                    }
                });
        });
    }

    sortLearningObjectives() {
        this.learningobjectives.sort((a, b) => {
            var x = a.Description.toLowerCase();
            var y = b.Description.toLowerCase();
            if (x < y) { return -1; }
            if (x > y) { return 1; }
            return 0;
        });

        this.learningobjectives.sort((x, y) => {
            if (x.IsSelected == y.IsSelected) return 0;
            if (x.IsSelected == true) return -1;
            return 1;
        });
    }

    get selectedOptions() {
        return this.learningobjectives.filter(opt => opt.IsSelected);
    }

    cbClickFlag = false;
    public clickHandler(event: any, index: number) {
        this.loChange(event.checked, index);
    }

    public setAccordianItemState(context: PreReflectionComponent, index: number = 0, checked: boolean = null) {
        if (context.selectedOptions.length > 2) {
            context.learningobjectives.forEach((value, index) => {
                context.accordion.enableItem(index, value.IsSelected);
            });
        }
        else {
            context.learningobjectives.forEach((value, index) => {
                context.accordion.enableItem(index, true);
            });
        }

        //To avoide auto page scrolling. Without this line, page will scroll down to the last accordion item upon check and uncheck.
        context.accordion.enableItem(index, true);
    }

    public ExpandAccordian() {
        this.learningobjectives.forEach((value, index) => {
            this.accordion.expandItem(value.IsSelected || this.stage != ActivityStatusTypes.PRE.Code, index);
        });
    }

    public getCSSClass(stage: string, learningobjective: LearningObjective) {
        if (stage == ActivityStatusTypes.PRE.Code) {
            if (!learningobjective.IsSelected) {
                return "";
            }
            return learningobjective.prerefstatus ? "border-bottom-red" : "";
        }
        else {
            return learningobjective[stage == ActivityStatusTypes.MID.Code ? 'midrefstatus' : 'postrefstatus'] ? "" : "border-bottom-red";
        }
    }

    public updateMidRefStatus(status: boolean, index: number): void {
        this.learningobjectives[index].midrefstatus = status;
    }

    public updatePostRefStatus(status: boolean, index: number): void {
        this.learningobjectives[index].postrefstatus = status;
    }

    public getStatus(id: string) {
        if (this.allDisabled) {
            return true;
        }

        if (this.learningobjectives.filter(lo => lo.IsSelected).length < 3) {
            return false;
        }
        else {
            return !this.learningobjectives.find(lo => lo.LearningObjectiveId == id).IsSelected;
        }
    }

    public updatePreRefStatus(status: boolean, index: number): void {
        this.learningobjectives[index].prerefstatus = status;
    }

    loaded() {
        this.manualSave = false;
        this.valueChanged.emit(true);
        if (!this.timeoutSet && !this.showValidationMessage(false)) {
            setTimeout(() => {
                if (!this.inProgress && !this.manualSave) {
                    this.onSubmit();
                    this.timeoutSet = false;
                }
            }, SYSTEM_CONSTANTS.AUTOSAVE_TIMER);
        }
    }

    showValidationMessage(submit: boolean): boolean {
        if (!submit) {
            this.validationMessage = Helper.getActivityUpdateValidationMessage(this.form, this.stage);

            if (this.validationMessage) {
                this.inProgress = false;
                this.manualSave = true;
                this.validationDialog.show();
                return true;
            }

            return false;
        }
    }

    public validationOkClick: EmitType<object> = () => {
        this.validationDialog.hide();
    }

    public validationDlgButtons: Object[] = [{ click: this.validationOkClick.bind(this), buttonModel: { content: 'Ok' } }];

    public loChangeDlgCancelClick: EmitType<object> = () => {
        this.loChange(!this.loChangeEvent, this.loChangeIndex);
        this.loChangeDialog.hide();
    }

    public loChangeDlgOkClick: EmitType<object> = () => {
        this.loChange(this.loChangeEvent, this.loChangeIndex);
        this.loChangeDialog.hide();
    }

    loChange(checked: boolean, index: number) {
        this.learningobjectives[index].IsSelected = checked;
        let group = [];
        let qna = [];
        this.learningobjectives.forEach((lo, myIndex) =>
        {

            if (myIndex == index) {
                lo.LearningObjectiveQuestions.forEach(loq => qna.push({ Answer: "" }));
                group.push({ IsSelected: lo.IsSelected, LearningObjectiveQuestions: qna });
            }
            else {
                group.push({ IsSelected: lo.IsSelected });
            }
        });

        this.form.patchValue({ LearningObjectives: group });
        this.accordion.expandItem(checked, index);
        this.loqs.find(o => o.index == index).contentUpdated();
        this.setAccordianItemState(this, index, checked);
    }
}