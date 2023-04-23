import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/common/api/models/user';
import { UserService } from 'src/app/common/api/services';
import { getErrorMessage } from 'src/app/common/helpers';

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

  public constructor(private userService: UserService, private router: Router) { }

  public ngOnInit(): void {
    this.createLoginForm();
  }

  public submit(): void {
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
    this.isRequestInProgress = false;

    this.router.navigateByUrl("/");
  }

  private handleLoginError(error: HttpErrorResponse): void {
    this.isRequestInProgress = false;

    this.errorMessage = getErrorMessage(error);;
  }
}
