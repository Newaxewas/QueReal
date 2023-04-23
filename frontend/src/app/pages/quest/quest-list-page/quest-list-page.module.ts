import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestListPageComponent } from './quest-list-page/quest-list-page.component';
import { RouterModule, Routes } from '@angular/router';
import { MatListModule } from "@angular/material/list";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { AppCommonModule } from 'src/app/common/app-common.module';
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";

const routes: Routes = [
  {
    path: '',
    component: QuestListPageComponent
  }
];

@NgModule({
  declarations: [
    QuestListPageComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    AppCommonModule,
    MatListModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
  ]
})
export class QuestListPageModule {

}
