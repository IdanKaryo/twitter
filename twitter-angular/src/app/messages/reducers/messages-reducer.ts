import {initialState, MessagesState} from '../state/messages-state';
import {SetHasNewMessagesAction, MessagesActionTypes, SetMessagesAction, SetUsernameFilterAction} from '../actions/messages-actions';

export function messagesReducer(state: MessagesState = initialState, action): MessagesState {
    switch (action.type) {
        case MessagesActionTypes.SET_HAS_NEW_MESSAGES:
            return {
                ...state,
                hasNewMessages: (<SetHasNewMessagesAction>action).hasNewMessages
            };
        case MessagesActionTypes.SET_MESSAGES:
            return {
                ...state,
                messages: (<SetMessagesAction>action).messages
            };
        case MessagesActionTypes.SET_USERNAME_FILTER:
            return {
                ...state,
                userNameFilter: (<SetUsernameFilterAction>action).userNameFilter
            };
        default:
            return state;
    }
}
