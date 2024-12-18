import { Injectable } from '@angular/core';
import { Activity, OverallSummary } from '../activity/activity';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SYSTEM_CONSTANTS, LocalStorageKeys } from '../shared/system.constants';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LearningObjective, ActivityQuestionAnswer } from '../activity/activity-detail/learning-objective';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
    private getAuthTokenUrl = 'oauth/token';

    constructor(private http: HttpClient) {
    }

    getAuthToken(): Observable<any> {
        const url = SYSTEM_CONSTANTS.WEBSITE_BASE_URL + `${this.getAuthTokenUrl}`;
        let param = "grant_type=client_credentials&client_id=099153c2625149bc8ecb3e85e03f0022&client_secret=dummy";
        return this.http.post<any>(url, param, httpOptions)
            .pipe(
                catchError(this.handleError('getAuthToken', null))
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
