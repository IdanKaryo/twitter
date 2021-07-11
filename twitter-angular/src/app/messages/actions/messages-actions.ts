import {Action} from '@ngrx/store';
import {Message} from '../../models/message';

export enum MessagesActionTypes {
    SET_HAS_NEW_MESSAGES = '[MESSAGES] Has new messages',
    SET_MESSAGES = '[MESSAGES] New messages',
    SET_USERNAME_FILTER = '[MESSAGES] User name filter',
    LOAD_MESSAGES = '[MESSAGES] Load messages',
    POST_NEW_MESSAGE = '[MESSAGES] Post new message'
}

export class SetHasNewMessagesAction implements Action {
    readonly type = MessagesActionTypes.SET_HAS_NEW_MESSAGES;
    constructor(public hasNewMessages: boolean) {}
}

export class SetMessagesAction implements Action {
    readonly type = MessagesActionTypes.SET_MESSAGES;
    constructor(public messages: Message[]) {}
}

export class SetUsernameFilterAction implements Action {
    readonly type = MessagesActionTypes.SET_USERNAME_FILTER;
    constructor(public userNameFilter: string) {}
}

export class PostNewMessageAction implements Action {
    readonly type = MessagesActionTypes.POST_NEW_MESSAGE;
    constructor(public message: string) {}
}

export class LoadMessagesAction implements Action {
    readonly type = MessagesActionTypes.LOAD_MESSAGES;
    constructor() {}
}



