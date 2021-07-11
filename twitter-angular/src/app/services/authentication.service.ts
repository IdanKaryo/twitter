import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {User} from '../models/user';
import {environment} from '../../environments/environment';

export const USER_STORAGE_KEY = 'currentUser';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(this.getUserFromStorage());
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    public signup(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/Authentication/SignUp`, { username, password });
    }

    public login(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/Authentication/SignIn`, { username, password })
            .pipe(map(user => {

                this.addUserToStorage(user);
                this.currentUserSubject.next(user);

                return user;
            }));
    }

    public logout() {
        this.deleteUserFromStorage();
        this.currentUserSubject.next(null);
    }

    private getUserFromStorage() {
        return JSON.parse(localStorage.getItem(USER_STORAGE_KEY));
    }

    private deleteUserFromStorage() {
        localStorage.removeItem(USER_STORAGE_KEY);
    }

    private addUserToStorage(user) {
        localStorage.setItem(USER_STORAGE_KEY, JSON.stringify(user));
    }
}
