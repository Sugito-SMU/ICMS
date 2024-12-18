import { Injectable } from '@angular/core';
import { Activity, UpdateActivityStatus } from '../activity/activity';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SYSTEM_CONSTANTS, LocalStorageKeys } from '../shared/system.constants';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LearningObjective } from '../activity/activity-detail/learning-objective';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
    private getFAQLinksUrl = 'common/getfaqlinks';
    private getActivitiesUrl = 'activities/getactivitiesbytype';
    private getOverallSummaryUrl = 'activities/getoverallsummarybytype';
    private getActivityUrl = 'activities/getactivitydetails';
    private getActivityStatusesUrl = 'common/getactivitystatuses';
    private getActivityUpdateStatusUrl = 'activities/updateactivitystatus';
    private getLearningObjectivesUrl = 'learningobjective/getlearningobjectives';
    private getActivityQuestionsUrl = 'activities/getactivityquestions';
    private getPostReflectionHeaderUrl = 'activities/getPostReflectionHeader';
    private getActivityUpdateUrl = 'learningobjective/updateactivitylearningobjectives';
    private studentId;

    constructor(private http: HttpClient) {
        this.studentId = localStorage.getItem(LocalStorageKeys.StudentId);
    }

    getFAQLinks(): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getFAQLinksUrl}`;
        return this.http.get<any>(url)
            .pipe(
                catchError(this.handleError('getFAQLinks', null))
            );
    }

    getActivities(type: string): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getActivitiesUrl}?studentId=${this.studentId}&typecode=${type}`;
        return this.http.get<any>(url)
            .pipe(
                catchError(this.handleError('getActivities', null))
            );
    }

    getOverallSummary(type: string): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getOverallSummaryUrl}?studentId=${this.studentId}&typecode=${type}`;
        return this.http.get<any>(url)
            .pipe(
            catchError(this.handleError('getOverallSummary', null))
            );
    }

    getActivity(id: string, student_id: string): Observable<Activity> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getActivityUrl}?studentId=${student_id}&id=${id}`;
        return this.http.get<Activity>(url)
            .pipe(
                catchError(this.handleError<Activity>('getActivity'))
            );
    }

    getActivityStatuses(currentStatus: string): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getActivityStatusesUrl}?currentStatus=${currentStatus}`;
        return this.http.get<any>(url)
            .pipe(
            catchError(this.handleError('getActivityStatuses', null))
            );
    }

    updateActivityStatus(updateStatus: UpdateActivityStatus): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getActivityUpdateStatusUrl}`;
        return this.http.post<any>(url, updateStatus, httpOptions)
            .pipe(
                catchError(this.handleError('updateActivityStatus', null))
            );
    }

    getLearningObjectives(activityid: string, stage: string, student_id: string): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getLearningObjectivesUrl}?studentId=${student_id}&activityId=${activityid}&stage=${stage}`;
        return this.http.get<any>(url)
            .pipe(
                catchError(this.handleError('getLearningObjectives', null))
            );
    }

    getActivityQuestions(activityid: string, student_id: string): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getActivityQuestionsUrl}?studentId=${student_id}&activityId=${activityid}`;
        return this.http.get<any>(url)
            .pipe(
                catchError(this.handleError('getActivityQuestions', null))
            );
    }

    getPostReflectionHeader(activityid: string): Observable<string> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getPostReflectionHeaderUrl}?studentId=${this.studentId}&activityId=${activityid}`;
        return this.http.get<string>(url)
            .pipe(
                catchError(this.handleError('getPostReflectionHeader', ""))
            );
    }
    
    updateActivity(activity: LearningObjective[]): Observable<any> {
        const url = SYSTEM_CONSTANTS.API_BASE_URL + `${this.getActivityUpdateUrl}?studentId=${this.studentId}`;
        return this.http.post<any>(url, activity, httpOptions)
            .pipe(
                catchError(this.handleError('updateActivity', null))
            );
    }

    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            //this.toastr.error('Error in ' + operation);
            console.error(error); // log to console instead

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }
}
