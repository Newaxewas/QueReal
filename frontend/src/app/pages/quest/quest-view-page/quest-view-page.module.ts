import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestViewPageComponent } from './quest-view-page/quest-view-page.component';
import { RouterModule, Routes } from '@angular/router';
import { questResolver } from 'src/app/resolvers';
import { QuestItemComponent } from './quest-item/quest-item.component';
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { ReactiveFormsModule } from "@angular/forms";

const routes: Routes = [
  {
    path: '',
    component: QuestViewPageComponent,
    resolve: { questGetResponse: questResolver },
  }
];

@NgModule({
  declarations: [
    QuestViewPageComponent,
    QuestItemComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatProgressBarModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
  ]
})
export class QuestViewPageModule { }
