import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {ChannelProviderService} from '../../services/channel-provider.service';
import {Store} from '@ngrx/store';
import {LoadMessagesAction, PostNewMessageAction, SetHasNewMessagesAction, SetUsernameFilterAction} from '../actions/messages-actions';
import {Observable} from 'rxjs';
import {AppState} from '../../models/app-state';
import {Message} from '../../models/message';

@Injectable({providedIn: 'root'})
export class MessageService {

    static MESSAGES_CHANNEL_NAME = 'BroadcastMessage';

    constructor(private http: HttpClient,
                private store: Store<AppState>,
                private channelProviderService: ChannelProviderService) { }

    public get messages$(): Observable<Message[]> {
        return this.store.select(state => state.messagesState.messages);
    }

    public get hasNewMessages$(): Observable<boolean> {
        return this.store.select(state => state.messagesState.hasNewMessages);
    }

    public postMessage(message: string) {
        return this.store.dispatch(new PostNewMessageAction(message));
    }

    public loadMessages() {
        return this.store.dispatch(new LoadMessagesAction());
    }

    public setUsernameFilter(usernameFilter) {
        return this.store.dispatch(new SetUsernameFilterAction(usernameFilter));
    }



    public subscribeToNewMessagesChannel() {

        this.channelProviderService.subscribeToChannel(MessageService.MESSAGES_CHANNEL_NAME, () => {
            this.store.dispatch(new SetHasNewMessagesAction(true));
        });
    }

    public unsubscribeToNewMessages() {

        this.channelProviderService.unsubscribe(MessageService.MESSAGES_CHANNEL_NAME);
    }
}
