import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule} from '@angular/router';
import {AppRoutingModule} from './app.routing';

import {AppComponent} from './app.component';
import {SignupComponent} from './signup/signup.component';
import {NavbarComponent} from './shared/navbar/navbar.component';
import {FooterComponent} from './shared/footer/footer.component';

import {LoginComponent} from './login/login.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {MessagesComponent} from './messages/messages.component';
import {TokenInterceptor} from './helpers/token.interceptor';
import {ErrorInterceptor} from './helpers/error.interceptor';
import {VirtualScrollerModule} from 'ngx-virtual-scroller';
import {MessageComponent} from './messages/message/message.component';
import {ActionReducerMap, StoreModule} from '@ngrx/store';
import {AppState} from './models/app-state';
import {messagesReducer} from './messages/reducers/messages-reducer';
import {EffectsModule} from '@ngrx/effects';
import {MessagesEffects} from './messages/effects/messages-effects';

export const reducers: ActionReducerMap<AppState> = {
    messagesState: messagesReducer,
};

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    NavbarComponent,
    FooterComponent,
    LoginComponent,
    MessagesComponent,
    MessageComponent
  ],
    imports: [
        BrowserModule,
        NgbModule,
        FormsModule,
        RouterModule,
        AppRoutingModule,
        HttpClientModule,
        ReactiveFormsModule,
        VirtualScrollerModule,
        StoreModule.forRoot(reducers),
        EffectsModule.forRoot([MessagesEffects]),
    ],
  providers: [
      { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
