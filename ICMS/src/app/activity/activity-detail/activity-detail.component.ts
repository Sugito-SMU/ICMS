import { Component, OnInit, ViewEncapsulation, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { DialogComponent } from '@syncfusion/ej2-angular-popups';
import { TabComponent } from '@syncfusion/ej2-angular-navigations';
import { ActivatedRoute } from '@angular/router';
import { EmitType } from '@syncfusion/ej2-base';
import { Router } from '@angular/router';

import { Activity, UpdateActivityStatus } from '../activity';
import { ActivityService } from 'src/app/services/activity.service';
import { ActivityReflectionCodes, ActivityStatusTypes, ActivityTypes, LocalStorageKeys } from 'src/app/shared/system.constants';
import { SelectEventArgs } from '@syncfusion/ej2-angular-navigations';
import { PreReflectionComponent } from './pre-reflection/pre-reflection.component';
import { ToastrService } from 'ngx-toastr';
import { ValidationErrors } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';
import { Helper } from 'src/app/shared/helpers/icms.helper';
import { DropDownListComponent } from '@syncfusion/ej2-angular-dropdowns';

@Component({
  selector: 'app-activity-detail',
  templateUrl: './activity-detail.component.html',
  styleUrls: [
    './activity-detail.component.css',
    '../../../../node_modules/@syncfusion/ej2-angular-navigations/styles/material.css'
  ],
  encapsulation: ViewEncapsulation.None
})
export class ActivityDetailComponent implements OnInit {
    // Define Dialog properties
    @ViewChild('confirmBackDialog')
    public confirmBackDialog: DialogComponent;
    @ViewChild('confirmSubmitDialog')
    public confirmSubmitDialog: DialogComponent;
    @ViewChild('confirmSubmitValidationDialog')
    public confirmSubmitValidationDialog: DialogComponent;
    @ViewChild('validationDialog')
    public validationDialog: DialogComponent;
    @ViewChild('confirmationDialog')
    public confirmationDialog: DialogComponent;
    @ViewChild('ddlStatus')
    public ddlStatus: DropDownListComponent;

    @ViewChild('tab')
    public tab: TabComponent;

    @ViewChild('preActivity')
    public preActivity: PreReflectionComponent;
    @ViewChild('midActivity')
    public midActivity: PreReflectionComponent;
    @ViewChild('postActivity')
    public postActivity: PreReflectionComponent;
    @ViewChildren(PreReflectionComponent) preRefComp: QueryList<PreReflectionComponent>;

    readonly ActivityStatusTypes = ActivityStatusTypes;
    readonly ActivityReflectionCodes = ActivityReflectionCodes;

    public validationHeader: string = 'Incomplete';
    public confirmHeader: string = 'Are you sure?';
    public confirmWidth: string = '400px';
    public confirmCloseIcon: Boolean = true;
    public hidden: Boolean = false;
    public isModal: Boolean = true;
    public animationSettings: Object = { effect: 'None' };
    public position: object = { X: 'center', Y: 'center' };
    showBackMessage: boolean = false;

    public currenttab = null;
    private id: string;
    public activitydata: Activity[] = [];
    public statusList;
    public fields: Object = { text: 'Description', value: 'Code' };
    activitytitle = "";
    desctitle = "";
    load: boolean = false;
    showValidation: boolean = false;
    isAdmin: boolean = false;
    message: string = "Please complete all answers before submitting."
    validationMessage: string = this.message;
    changeStatusValidationMessage: string = "Please select status before submitting.";
    changeStatusConfirmationMessage: string = "You are going to change status of the student reflection. Please confirm.";

    public headerText: Object[] = [{ 'text': 'Summary' }, { 'text': ActivityStatusTypes.PRE.Description }, { 'text': ActivityStatusTypes.MID.Description }, { 'text': ActivityStatusTypes.POST.Description }];

    constructor(private route: ActivatedRoute, private router: Router, private activityService: ActivityService,
        private toastr: ToastrService, private authenticationService: AuthenticationService, public sharedService: SharedService) { }

    ngOnInit() {
        this.id = this.route.snapshot.paramMap.get('id');
        this.isAdmin = localStorage.getItem(LocalStorageKeys.IsAdmin) == 'True';

        this.authenticationService.getAuthToken().subscribe(response => {
            if (response) {
                this.sharedService.jsonData = response;
                this.getActivityById();
            }
        });
    }

    getActivityStatuses() {
        this.activityService.getActivityStatuses(this.activitydata[0].ReflectionStatusCode).subscribe(statuses => this.statusList = statuses.result);
    }

    getActivityById() {

        let student_id = localStorage.getItem(LocalStorageKeys.StudentId);

        if (this.isAdmin) {
            student_id = this.route.snapshot.paramMap.get('studentId');
        }

        this.activityService.getActivity(this.id, student_id).subscribe(activity => {
            if (activity) {
                this.activitydata.push(activity);
                if (this.isAdmin) {
                    this.getActivityStatuses();
                }
                this.doPostProcessing(activity);
                this.load = true;
            }
            else {
                this.router.navigate(['/activity/home/']);
            }
        });
    }

    submit(submit: boolean = false) {
        localStorage.setItem(LocalStorageKeys.RefreshDisplayTab, this.currenttab);
        switch (this.currenttab) {
            case ActivityStatusTypes.PRE.Code:
                this.preActivity.onSubmit(submit);
                break;
            case ActivityStatusTypes.MID.Code:
                this.midActivity.onSubmit(submit);
                break;
            case ActivityStatusTypes.POST.Code:
                this.postActivity.onSubmit(submit);
                break;
        }
    }

    get showButtons() {
        if (this.isAdmin) {
            return false;
        }

        switch (this.currenttab) {
            case ActivityStatusTypes.PRE.Code:
                return this.activitydata[0].EnablePreActivity;
            case ActivityStatusTypes.MID.Code:
                return this.activitydata[0].EnableMidActivity;
            case ActivityStatusTypes.POST.Code:
                return this.activitydata[0].EnablePostActivity;
        }

        return true;
    }

    get getForm() {
        switch (this.currenttab) {
            case ActivityStatusTypes.PRE.Code:
                return this.preActivity ? this.preActivity.form : null;
            case ActivityStatusTypes.MID.Code:
                return this.midActivity ? this.midActivity.form : null;
            case ActivityStatusTypes.POST.Code:
                return this.postActivity ? this.postActivity.form : null;
        }
    }

    public tabSelected(e: SelectEventArgs) {
        this.currenttab = this.headerText[e.selectedIndex]['text'] == "Summary" ? null : ActivityStatusTypes.getStatusCodeByDescription(this.headerText[e.selectedIndex]['text']);
    }

    doPostProcessing(activity: Activity) {
        if (activity != null) {
            switch (activity.ActivityTypeCode) {
                case ActivityTypes.CommunityService:
                    this.desctitle = "Project Title";
                    break;
                case ActivityTypes.Internship:
                    this.desctitle = "Internship Title";
                    break;
                default:
                    this.activitytitle = "Summary";
            }
            this.activitytitle = activity.ActivityTypeDescription + " - " + activity.PositionTitle;

            //hide tabs
            if (!activity.ShowPostActivity) {
                this.headerText.splice(3, 1);
                if (!activity.ShowMidActivity) {
                    this.headerText.splice(2, 1);
                    if (!activity.ShowPreActivity) {
                        this.headerText.splice(1, 1);
                    }
                }
            }

            setTimeout(() => {
                let tabToDisplay = localStorage.getItem(LocalStorageKeys.RefreshDisplayTab);
                if (tabToDisplay) {
                    let indexToShow = this.headerText.findIndex(ht => ht["text"] == ActivityStatusTypes.getStatusDescriptionByCode(tabToDisplay));
                    this.tab.select(indexToShow);
                    localStorage.removeItem(LocalStorageKeys.RefreshDisplayTab);
                }

                let messageToDisplay = localStorage.getItem(LocalStorageKeys.RefreshToastMessage);
                if (messageToDisplay) {
                    this.toastr.success(localStorage.getItem(LocalStorageKeys.RefreshToastMessage));
                    localStorage.removeItem(LocalStorageKeys.RefreshToastMessage);
                }
            }, 500);
            
            //set parameters in local storage to disable tabs
            localStorage.setItem(LocalStorageKeys.EnablePreActivity, `${!activity.EnablePreActivity}`);
            localStorage.setItem(LocalStorageKeys.EnableMidActivity, `${!activity.EnableMidActivity}`);
            localStorage.setItem(LocalStorageKeys.EnablePostActivity, `${!activity.EnablePostActivity}`);
        }
    }

    public confirmBackClick: EmitType<object> = () => {
        if (this.showBackMessage) {
            this.confirmBackDialog.show();
        }
        else {
            this.router.navigate(['/activity/home']);
        }
    }

    public confirmSubmitClick: EmitType<object> = () => {
        if (!this.getForm.valid) {
            this.showValidation = true;

            if (this.currenttab == ActivityStatusTypes.PRE.Code) {
                let msg = Helper.getActivityUpdateValidationMessage(this.getForm, this.currenttab, true);

                if (msg) {
                    this.validationMessage = msg;
                }
                else {
                    this.validationMessage = this.message;
                }
            }

            this.confirmSubmitValidationDialog.show();
        }
        else {
            this.confirmSubmitDialog.show();
        }
    }

    public confirmDlgBackNoClick: EmitType<object> = () => {
        this.confirmBackDialog.hide();
    }

    public confirmDlgBackYesClick: EmitType<object> = () => {
        this.router.navigate(['/activity/home']);
    }

    public confirmDlgSubmitNoClick: EmitType<object> = () => {
        this.confirmSubmitDialog.hide();
    }

    public confirmDlgSubmitYesClick: EmitType<object> = () => {
        this.submit(true);
        this.confirmSubmitDialog.hide();
    }

    public confirmDlgSubmitValidationOkClick: EmitType<object> = () => {
        this.confirmSubmitValidationDialog.hide();
    }

    public ChangeStatusValidationOkClick: EmitType<object> = () => {
        this.validationDialog.hide();
    }

    public ChangeStatusConfirmCancelClick: EmitType<object> = () => {
        this.confirmationDialog.hide();
    }

    public ChangeStatusConfirmOkClick: EmitType<object> = () => {
        this.changeStatus();
        this.confirmationDialog.hide();
    }

    public confirmBackDlgButtons: Object[] = [{ click: this.confirmDlgBackNoClick.bind(this), buttonModel: { content: 'No' } }, { click: this.confirmDlgBackYesClick.bind(this), buttonModel: { content: 'Yes', isPrimary: true } }];

    public confirmSubmitDlgButtons: Object[] = [{ click: this.confirmDlgSubmitNoClick.bind(this), buttonModel: { content: 'No' } }, { click: this.confirmDlgSubmitYesClick.bind(this), buttonModel: { content: 'Yes', isPrimary: true } }];

    public confirmSubmitValidationDlgButtons: Object[] = [{ click: this.confirmDlgSubmitValidationOkClick.bind(this), buttonModel: { content: 'Ok' } }];

    public changeStatusValidationDlgButtons: Object[] = [{ click: this.ChangeStatusValidationOkClick.bind(this), buttonModel: { content: 'Ok' } }];
    
    public changeStatusConfirmDlgButtons: Object[] = [{ click: this.ChangeStatusConfirmCancelClick.bind(this), buttonModel: { content: 'Cancel' } }, { click: this.ChangeStatusConfirmOkClick.bind(this), buttonModel: { content: 'Confirm', isPrimary: true } }];

    public valueChanged(status: boolean) {
        this.showBackMessage = status;
    }

    showChangeStatusDialog() {
        if (this.ddlStatus.value != null) {
            this.confirmationDialog.show();
        }
        else {
            this.load = true;
            this.validationDialog.show();
        }
    }

    changeStatus() {
        this.load = false;
        let updateStatus = new UpdateActivityStatus();
        updateStatus.ActivityId = this.id;
        updateStatus.Status = this.ddlStatus.value.toString();
        updateStatus.StudentId = this.route.snapshot.paramMap.get('studentId')
        this.activityService.updateActivityStatus(updateStatus).subscribe(response => {
            if (response) {
                window.location.reload();
            }
            else {
                this.router.navigate(['/activity/home/']);
            }
        });
    }
}
