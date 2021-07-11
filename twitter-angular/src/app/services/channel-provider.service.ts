import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {environment} from '../../environments/environment';
import {AuthenticationService} from './authentication.service';

@Injectable({providedIn: 'root'})
export class ChannelProviderService {

    connected: boolean;
    connection: signalR.HubConnection;

    constructor(private authenticationService: AuthenticationService) {}

    public subscribeToChannel(channelName: string, callback: () => any) {
        if (!this.connected) {
            this.buildConnection();
            this.startConnection();
        }

        this.connection.on(channelName, () => {
            callback();
        });
    }

    public unsubscribe(channelName: string) {
        if (this.connection) {
            this.connection.off(channelName);
        }
    }

    private startConnection() {
        this.connection.start().then(function () {
            console.log('SignalR Connected!');
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    private buildConnection() {
        this.connection =  new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Information)
            .withUrl(environment.apiUrl + '/notify', {
                withCredentials: false,
                accessTokenFactory: () => this.authenticationService.currentUserValue.token
            })
            .build();
    }
}
