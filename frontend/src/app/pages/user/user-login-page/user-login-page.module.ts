import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLoginPageComponent } from './user-login-page/user-login-page.component';
import { RouterModule, Routes } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from "@angular/material/button";

const routes: Routes = [
  {
    path: '',
    component: UserLoginPageComponent
  }
];

@NgModule({
  declarations: [
    UserLoginPageComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatButtonModule,
  ]
})
export class UserLoginPageModule {}
