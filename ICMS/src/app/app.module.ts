import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/helpers/jwt.interceptor';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
      AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
      AppRoutingModule,
      ToastrModule.forRoot({
          timeOut: 5000,
          preventDuplicates: true,
          maxOpened: 3,
          autoDismiss: true,
          closeButton: true,
          progressBar: true,
          progressAnimation: 'decreasing'
      })
  ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
