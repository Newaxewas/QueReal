import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterPageComponent } from './user-register-page/user-register-page.component';
import { RouterModule, Routes } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: UserRegisterPageComponent
  }
];

@NgModule({
  declarations: [
    UserRegisterPageComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
  ]
})
export class UserRegisterPageModule { }
