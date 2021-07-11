import {User} from './user';

export interface Message {
    insertionTime: Date;
    text: string;
    user: User;
}
