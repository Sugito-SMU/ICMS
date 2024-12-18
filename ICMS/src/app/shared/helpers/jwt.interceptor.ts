import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/services/shared.service';
import { LocalStorageKeys } from '../system.constants';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private sharedService: SharedService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.url.toLowerCase() != localStorage.getItem(LocalStorageKeys.WebsiteEndPoint).toLowerCase() + "oauth/token") {
            // add authorization header with jwt token if available
            let currentUser = this.sharedService.jsonData;
            if (currentUser && currentUser.access_token) {
                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${currentUser.access_token}`
                    }
                });
            }
        }

        return next.handle(request);
    }
}