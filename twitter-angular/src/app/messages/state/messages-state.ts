import {Message} from '../../models/message';

export interface MessagesState {
    hasNewMessages: boolean;
    userNameFilter: string;
    messages: Message[];
}

export const initialState: MessagesState = {
    hasNewMessages: false,
    userNameFilter: '',
    messages: []
};

