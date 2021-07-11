import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {Router} from '@angular/router';
import {AuthenticationService} from '../services/authentication.service';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

    focus;
    focus2;

    loginForm: FormGroup;
    error = '';

    constructor(private formBuilder: FormBuilder,
                private router: Router,
                private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    get formControls() { return this.loginForm.controls; }

    onSubmit() {

        if (this.loginForm.invalid) {
            return;
        }

        this.authenticationService.signup(this.formControls.username.value, this.formControls.password.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.router.navigate(['/login']);
                },
                error: error => {
                    this.error = error;
                }
            });
    }
}
