import { Component, OnInit, ViewChild } from '@angular/core';
import { Activity, OverallSummary } from '../activity';
import { RowSelectEventArgs, GridComponent } from '@syncfusion/ej2-angular-grids';
import { Router } from '@angular/router';
import { ActivityService } from '../../services/activity.service'
import { ActivityTypes, LocalStorageKeys, SYSTEM_CONSTANTS } from 'src/app/shared/system.constants';

@Component({
    selector: 'app-select-activity',
    templateUrl: './select-activity.component.html',
    styleUrls: ['./select-activity.component.css']
})

export class SelectActivityComponent implements OnInit {
    @ViewChild('communityservice')
    public communityservice: GridComponent;
    @ViewChild('internship')
    public internship: GridComponent;

    public internshipdata: Activity[];
    public communityservicedata: Activity[];
    public faqlinks;
    internshipSummary: OverallSummary[];
    communityserviceSummary: OverallSummary[];
    internshiptitle = "Internship";
    communityservicetitle = "Community Service";

    readonly ActivityTypes = ActivityTypes;
    readonly PRO_BONO_PROG_CODE = SYSTEM_CONSTANTS.PRO_BONO_PROG_CODE;
    load: number = 0;
    isAdmin: boolean = false;

    constructor(private router: Router, private activityService: ActivityService) { }

    ngOnInit() {
        this.isAdmin = localStorage.getItem(LocalStorageKeys.IsAdmin) == 'True';

        this.getInternships();
        this.getCommunityServices();
        this.getFAQLinks();
    }

    getFAQLinks() {
        this.activityService.getFAQLinks().subscribe(activities => this.faqlinks = activities.result);
    }

    getInternships() {
        this.activityService.getActivities(ActivityTypes.Internship).subscribe(activities => { this.load++; this.internshipdata = activities.result; });
        this.activityService.getOverallSummary(ActivityTypes.Internship).subscribe(overallSummary => this.internshipSummary = overallSummary.result );
    }

    getCommunityServices() {
        this.activityService.getActivities(ActivityTypes.CommunityService).subscribe(activities => { this.load++; this.communityservicedata = activities.result; });
        this.activityService.getOverallSummary(ActivityTypes.CommunityService).subscribe(overallSummary => this.communityserviceSummary = overallSummary.result );
    }

    public onRowSelected(args: RowSelectEventArgs): void {
        if (args.data["Url"] != null) {
            window.open(args.data["Url"], "_blank");
        }
        else {
            this.router.navigate(['/activity/detail/' + args.data["ActivityId"]]);
        }
    }

    setEmptyMessage(e: any, type: string) {
        switch (type) {
            case ActivityTypes.CommunityService:
                (this.communityservice as any).defaultLocale.EmptyRecord = "You have no community service projects.";
                break;
            case ActivityTypes.Internship:
                (this.internship as any).defaultLocale.EmptyRecord = "You have no internships.";
                break;
        }
    }
}