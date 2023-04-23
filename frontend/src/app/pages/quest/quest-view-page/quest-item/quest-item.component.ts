import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { QuestItemGetResponse, QuestSetProgressRequest } from 'src/app/common/api/models/quest';
import { QuestItemProgressChangedEvent } from './quest-item-progress-changed-event';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { QuestService } from 'src/app/common/api/services';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from 'src/app/common/helpers';

@Component({
  selector: 'app-quest-item',
  templateUrl: './quest-item.component.html',
  styleUrls: ['./quest-item.component.css']
})
export class QuestItemComponent implements OnInit {
  public isRequestInProgress = false;
  public errorMessage: string | null = null;

  public isEditMode = false;
  public questItemEditForm: FormGroup<{
    progress: FormControl<number>,
  }> = null!;

  @Input()
  public questItem: QuestItemGetResponse = null!;

  @Input()
  public canEdit: boolean = null!;

  @Output()
  public questItemProgressChange = new EventEmitter<QuestItemProgressChangedEvent>();

  public constructor(private questService: QuestService) { }

  public ngOnInit(): void {
    this.createQuestItemEditForm();
  }

  public startEditProgress(): void {
    this.isEditMode = true;
    this.questItemEditForm.controls.progress.setValue(this.questItem.progress);
  }

  public stopEditProgress(): void {
    this.isEditMode = false;
  }

  public saveProgress(): void {
    const formValue = this.questItemEditForm.value;

    const request = new QuestSetProgressRequest();
    request.questItemId = this.questItem.id;
    request.progress = formValue.progress!;

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.questService.setProgress(request).subscribe({
      next: ()=> this.handleSaveSuccess(request),
      error: this.handleSaveError.bind(this)
    });
  }

  private createQuestItemEditForm(): void {
    this.questItemEditForm = new FormGroup({
      progress: new FormControl<number>(this.questItem.progress, { nonNullable: true, validators: [Validators.required, Validators.min(0), Validators.max(100)] })
    })
  }

  private handleSaveSuccess(request: QuestSetProgressRequest): void {
    this.isRequestInProgress = false;

    this.questItemProgressChange.emit({ questItemId: request.questItemId, newProgress: request.progress })
    this.stopEditProgress();
  }

  private handleSaveError(error: HttpErrorResponse): void {
    this.isRequestInProgress = false;

    this.errorMessage = getErrorMessage(error);;
  }
}
