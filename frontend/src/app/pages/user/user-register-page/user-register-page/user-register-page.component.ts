import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterRequest } from 'src/app/common/api/models/user';
import { UserService } from 'src/app/common/api/services';
import { CustomValidators } from 'src/app/common/custom-validator'
import { getErrorMessage } from 'src/app/common/helpers';

@Component({
  selector: 'app-user-register-page',
  templateUrl: './user-register-page.component.html',
  styleUrls: ['./user-register-page.component.css']
})


export class UserRegisterPageComponent implements OnInit {
  public errorMessage: string | null = null;
  public isRequestInProgress: boolean = false;

  public userRegisterForm: FormGroup<{
    email: FormControl<string>,
    password: FormControl<string>,
    confirmPassword: FormControl<string>,
  }> = null!;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }
  
  public submit() {
    if (this.userRegisterForm.invalid) {
      return;
    }

    const formValue = this.userRegisterForm.value;

    const registerRequest = new RegisterRequest();
    registerRequest.email = formValue.email!;
    registerRequest.password = formValue.password!;
    registerRequest.confirmPassword = formValue.confirmPassword!;

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.userService.register(registerRequest)
      .subscribe({
        next: this.handleRegisterSuccess.bind(this),
        error: this.handleRegisterError.bind(this)
      });
  }

  private createRegisterForm(): void {
    const emailControl = new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.email] });
    const passwordControl = new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.minLength(8), Validators.maxLength(40)] });
    const confirmPasswordControl = new FormControl<string>("", { nonNullable: true, validators: [Validators.required, CustomValidators.matchValue(passwordControl)] });

    this.userRegisterForm = new FormGroup({
      email: emailControl,
      password: passwordControl,
      confirmPassword: confirmPasswordControl,
    });
  }

  private handleRegisterSuccess(): void {
    this.isRequestInProgress = false;

    this.router.navigateByUrl("/user/login");
  }

  private handleRegisterError(error: HttpErrorResponse): void {
    this.isRequestInProgress = false;

    this.errorMessage = getErrorMessage(error);;
  }
}
