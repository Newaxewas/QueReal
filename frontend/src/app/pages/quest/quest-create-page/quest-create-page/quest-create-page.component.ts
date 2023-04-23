import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuestCreateRequest, QuestCreateResponse, QuestItemCreateRequest } from 'src/app/common/api/models/quest';
import { QuestService } from 'src/app/common/api/services';
import { getErrorMessage } from 'src/app/common/helpers';

@Component({
  selector: 'app-quest-create-page',
  templateUrl: './quest-create-page.component.html',
  styleUrls: ['./quest-create-page.component.css']
})

export class QuestCreatePageComponent {
  public errorMessage: string | null = null;
  public isRequestInProgress: boolean = false;

  public questCreateForm: FormGroup<{
    questTitle: FormControl<string>,
    questItems: FormArray<FormControl<string>>;
  }> = null!;

  constructor(private questService: QuestService, private router: Router) { }

  ngOnInit(): void {
    this.createQuestCreateForm();
  }



  public submit() {
    if (this.questCreateForm.invalid) {
      return;
    }

    const formValue = this.questCreateForm.value;

    const questCreateRequest = new QuestCreateRequest();
    questCreateRequest.title = formValue.questTitle!
    questCreateRequest.questItems = formValue.questItems!.map(questItemTitle => {
      const questCreateItemRequest = new QuestItemCreateRequest();
      questCreateItemRequest.title = questItemTitle;

      return questCreateItemRequest;
    })

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.questService.create(questCreateRequest)
      .subscribe({
        next: this.handleCreateSuccess.bind(this),
        error: this.handleCreateError.bind(this)
      });
  }

  private createQuestCreateForm(): void {
    this.questCreateForm = new FormGroup({
      questTitle: new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.minLength(3), Validators.maxLength(50)] }),
      questItems: new FormArray<FormControl<string>>([]),
    });

    this.addQuestItem();
  }

  public addQuestItem(): void {
    this.questCreateForm.controls.questItems
      .push(new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.minLength(3), Validators.maxLength(40)] }));
  }

  public removeQuestItem(index: number): void {
    this.questCreateForm.controls.questItems
      .removeAt(index);
  }

  private handleCreateSuccess(response: QuestCreateResponse): void {
    this.isRequestInProgress = false;

    this.router.navigateByUrl(`/quest/view/${response.id}`);
  }

  private handleCreateError(error: HttpErrorResponse): void {
    this.isRequestInProgress = false;

    this.errorMessage = getErrorMessage(error);;
  }
}
