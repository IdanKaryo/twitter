import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {AuthenticationService} from '../services/authentication.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    focus;
    focus1;

    loading = false;
    error = '';
    loginForm: FormGroup;

    constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
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

        this.loading = true;
        this.authenticationService.login(this.formControls.username.value, this.formControls.password.value)
          .pipe(first())
          .subscribe({
              next: () => {
                  const url = this.route.snapshot.queryParams['returnUrl'] || '/';
                  this.router.navigate([url]);
                  this.loading = false;
              },
              error: error => {
                  this.error = error;
                  this.loading = false;
              }
          });
    }
}
