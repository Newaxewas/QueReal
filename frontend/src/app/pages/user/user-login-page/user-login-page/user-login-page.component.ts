import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/common/api/models/user';
import { UserService } from 'src/app/common/api/services';


@Component({
  selector: 'app-user-login-page',
  templateUrl: './user-login-page.component.html',
  styleUrls: ['./user-login-page.component.css']
})

export class UserLoginPageComponent implements OnInit {
  public errorMessage: string | null = null;
  public isRequestInProgress: boolean = false;

  public userLoginForm: FormGroup<{
    email: FormControl<string>,
    password: FormControl<string>,
    remember: FormControl<boolean>
  }> = null!;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.createLoginForm();
  }

  public submit() {
    if (this.userLoginForm.invalid) {
      return;
    }

    const formValue = this.userLoginForm.value;

    const loginRequest = new LoginRequest();
    loginRequest.email = formValue.email!;
    loginRequest.password = formValue.password!;
    loginRequest.remember = formValue.remember!;

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.userService.login(loginRequest)
      .subscribe({
        next: this.handleLoginSuccess.bind(this),
        error: this.handleLoginError.bind(this)
      });
  }

  private createLoginForm(): void {
    this.userLoginForm = new FormGroup({
      email: new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.email] }),
      password: new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.minLength(8), Validators.maxLength(40)] }),
      remember: new FormControl<boolean>(false, { nonNullable: true, validators: [Validators.required] })
    })
  }

  private handleLoginSuccess(): void {
    this.router.navigateByUrl("/");

    this.isRequestInProgress = false;
  }

  private handleLoginError(error: HttpErrorResponse): void {
    this.errorMessage = error.status === 0 ? "Connection error" : "Check your login and password";

    this.isRequestInProgress = false;
  }
}
