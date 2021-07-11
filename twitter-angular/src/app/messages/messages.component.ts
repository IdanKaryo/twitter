import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MessageService} from './services/message.service';
import {VirtualScrollerComponent} from 'ngx-virtual-scroller';
import {Subscription} from 'rxjs/Subscription';

@Component({
    selector: 'app-messages',
    templateUrl: './messages.component.html',
    styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit, OnDestroy {

    focus;
    postMessageForm: FormGroup;
    messagesSubscription: Subscription;

    @ViewChild('scroll', {static: false}) virtualScroller: VirtualScrollerComponent;

    constructor(private formBuilder: FormBuilder,
                private messageService: MessageService) { }

    ngOnInit() {
         this.initializeForm();

        this.messageService.loadMessages();
        this.messageService.subscribeToNewMessagesChannel();

        this.subscribeToMessages();
    }

    private subscribeToMessages() {
        this.messagesSubscription = this.messageService.messages$.subscribe(() => {
            this.scrollTop();
        });
    }

    private initializeForm() {
        this.postMessageForm = this.formBuilder.group({
            message: ['', Validators.required],
            filter: ['']
        });
    }

    get formControls() { return this.postMessageForm.controls; }

    onPostMessage() {
        this.messageService.postMessage(this.formControls.message.value);
        this.formControls.message.reset();
    }

    onReloadMessages() {
        this.messageService.loadMessages();
    }

    onFilterChange(value: string) {
        this.messageService.setUsernameFilter(value);
    }

    scrollTop() {
        if (this.virtualScroller) {
            this.virtualScroller.scrollToIndex(0);
        }
    }

    ngOnDestroy(): void {
        this.messageService.unsubscribeToNewMessages();
        this.messagesSubscription.unsubscribe();
    }
}
