import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestEditRequest, QuestGetResponse, QuestItemEditRequest } from 'src/app/common/api/models/quest';
import { QuestService } from 'src/app/common/api/services';

@Component({
  selector: 'app-quest-edit-page',
  templateUrl: './quest-edit-page.component.html',
  styleUrls: ['./quest-edit-page.component.css']
})
export class QuestEditPageComponent {
  public errorMessage: string | null = null;
  public isRequestInProgress: boolean = false;

  public quest: QuestGetResponse = null!;

  public questEditForm: FormGroup<{
    questTitle: FormControl<string>,
    questItems: FormArray<FormGroup<{
      id: FormControl<string | null>,
      title: FormControl<string>
    }>>;
  }> = null!;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private questService: QuestService) { }

  ngOnInit(): void {
    this.createQuestEditForm();
    this.activatedRoute.data.subscribe(({ quest }) => {
      this.quest = quest;
      this.initFormFieldValues()
    });
  }

  public submit() {
    if (this.questEditForm.invalid) {
      return;
    }

    const formValue = this.questEditForm.value;

    const questEditRequest = new QuestEditRequest();
    questEditRequest.id = this.quest.id;
    questEditRequest.title = formValue.questTitle!
    questEditRequest.questItems = formValue.questItems!.map(questItemGroup => {
      const questEditItemRequest = new QuestItemEditRequest();
      questEditItemRequest.id = questItemGroup.id ?? null;
      questEditItemRequest.title = questItemGroup.title!;

      return questEditItemRequest;
    })

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.questService.edit(questEditRequest)
      .subscribe({
        next: this.handleEditSuccess.bind(this),
        error: this.handleEditError.bind(this)
      });
  }

  private createQuestEditForm(): void {
    this.questEditForm = new FormGroup({
      questTitle: new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.minLength(3), Validators.maxLength(50)] }),
      questItems: new FormArray<FormGroup<{
        id: FormControl<string | null>,
        title: FormControl<string>
      }>>([]),
    });

    this.addQuestItem();
  }

  public addQuestItem(): void {
    this.questEditForm.controls.questItems
      .push(new FormGroup({
        id: new FormControl<string | null>(null),
        title: new FormControl<string>("", { nonNullable: true, validators: [Validators.required, Validators.minLength(3), Validators.maxLength(40)] })
      }));
  }

  public removeQuestItem(index: number): void {
    this.questEditForm.controls.questItems
      .removeAt(index);
  }

  private initFormFieldValues(): void {
    this.questEditForm.controls.questTitle.setValue(this.quest.title);

    this.questEditForm.controls.questItems.clear();

    for (let i = 0; i < this.quest.questItems.length; i++) {
      this.addQuestItem();
      
      const questItemValue = this.quest.questItems[i];
      const questItemGroup = this.questEditForm.controls.questItems.controls[i];

      questItemGroup.controls.id.setValue(questItemValue.id);
      questItemGroup.controls.title.setValue(questItemValue.title); 
    }
  }

  private handleEditSuccess(): void {
    this.router.navigateByUrl(`/quest/view/${this.quest.id}`);

    this.isRequestInProgress = false;
  }

  private handleEditError(error: HttpErrorResponse): void {
    this.errorMessage = error.status === 0 ? "Connection error" : "Something went wrong";

    this.isRequestInProgress = false;
  }
}
