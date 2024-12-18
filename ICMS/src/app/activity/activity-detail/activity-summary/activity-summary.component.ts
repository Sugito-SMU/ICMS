import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Activity } from '../../activity';
import { ActivityTypes, SYSTEM_CONSTANTS } from 'src/app/shared/system.constants';

@Component({
  selector: 'app-activity-summary',
  templateUrl: './activity-summary.component.html',
  styleUrls: ['./activity-summary.component.css']
})
export class ActivitySummaryComponent implements OnInit {
    @Input() details: Activity;

    public title: string;
    public unit: string;
    public description: string;
    public lblunit: string;
    show = false;

    readonly SHORT_DESC_LENGTH = SYSTEM_CONSTANTS.SHORT_DESC_LENGTH;
    length = this.SHORT_DESC_LENGTH;

  constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        switch (this.details.ActivityTypeCode) {
            case ActivityTypes.CommunityService:
                this.title = "Project Title";
                this.description = "Project Scope";
                break;
            case ActivityTypes.Internship:
                this.title = "Internship Title";
                this.description = "Job Summary";
                break;
        }
    }

    showMoreLess() {
        this.show = !this.show;
        if (this.show) {
            this.length = this.details.Description.length;
        }
        else {
            this.length = this.SHORT_DESC_LENGTH;
        }
    }

}
