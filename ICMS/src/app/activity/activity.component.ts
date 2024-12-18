import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

    constructor(private authenticationService: AuthenticationService, public sharedService: SharedService) { }

    ngOnInit() {
        this.authenticationService.getAuthToken().subscribe(response => {
            if (response) {
                this.sharedService.jsonData = response;
            }
        });
    }
}
