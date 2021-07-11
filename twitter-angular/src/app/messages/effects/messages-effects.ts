import {Injectable} from '@angular/core';
import {Actions, Effect, ofType} from '@ngrx/effects';
import {
    LoadMessagesAction,
    MessagesActionTypes,
    PostNewMessageAction,
    SetHasNewMessagesAction,
    SetMessagesAction, SetUsernameFilterAction
} from '../actions/messages-actions';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {AppState} from '../../models/app-state';
import {Store} from '@ngrx/store';
import {debounceTime, mergeMap, switchMap} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {Message} from '../../models/message';

@Injectable()
export class MessagesEffects {
    @Effect({ dispatch: false }) postMessage$ = this.actions$
        .pipe(
            ofType<PostNewMessageAction>(MessagesActionTypes.POST_NEW_MESSAGE),
            mergeMap((action) => {
                return this.http.post<any>(`${environment.apiUrl}/Message/PostMessage`, { message: action.message });
            })
        );

    @Effect() loadMessages$ = this.actions$
        .pipe(
            ofType<LoadMessagesAction>(MessagesActionTypes.LOAD_MESSAGES),
            mergeMap((action) => {
                return this.getMessages('');
            }),
            mergeMap((messages) => {
                return [new SetMessagesAction(messages), new SetHasNewMessagesAction(false)];
            })
        );

    @Effect() usernameFilterChange$ = this.actions$
        .pipe(
            ofType<SetUsernameFilterAction>(MessagesActionTypes.SET_USERNAME_FILTER),
            debounceTime(500),
            switchMap((action) => {
                const usernameFilter = (<SetUsernameFilterAction>action).userNameFilter;
                return this.getMessages(usernameFilter);
            }),
            mergeMap((messages) => {
                return [new SetMessagesAction(messages), new SetHasNewMessagesAction(false)];
            })
        );

    constructor(
        private actions$: Actions,
        private store: Store<AppState>,
        private http: HttpClient
    ) {}

    private getMessages(usernameFilter: string): Observable<Message[]> {
        return this.http.post<any>(`${environment.apiUrl}/Message/GetMessages`, { partialUsernameOrNothing: usernameFilter });
    }
}
