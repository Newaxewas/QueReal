import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestApproveCompletionRequest, QuestDeleteRequest, QuestGetResponse } from 'src/app/common/api/models/quest';
import { QuestItemProgressChangedEvent } from '../quest-item/quest-item-progress-changed-event';
import { QuestService } from 'src/app/common/api/services';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-quest-view-page',
  templateUrl: './quest-view-page.component.html',
  styleUrls: ['./quest-view-page.component.css']
})

export class QuestViewPageComponent implements OnInit {
  public isRequestInProgress = false;
  public errorMessage: string | null = null;

  public quest: QuestGetResponse = null!;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private questService: QuestService) { }

  get canApproveCompletion(){
    return this.quest.approvedTime === null 
      && this.quest.questItems.every(questItem => questItem.progress === 100);
  }

  get canEdit(){
    return this.quest.approvedTime === null;
  }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ questGetResponse }) => this.quest = questGetResponse);
  }

  handleQuestItemProgressChange(progressChangedEvent: QuestItemProgressChangedEvent): void {
    const questItem = this.quest.questItems.find(questItem => questItem.id === progressChangedEvent.questItemId)!;

    questItem.progress = progressChangedEvent.newProgress;
  }

  public approveCompletion(): void{
    const request = new QuestApproveCompletionRequest();
    request.id = this.quest.id;

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.questService.approveCompletion(request).subscribe({
      next: this.handleApproveCompletionSuccess.bind(this),
      error: this.handleError.bind(this)
    });
  }

  public editQuest(): void{
    this.router.navigateByUrl(`/quest/edit/${this.quest.id}`);
  }

  public deleteQuest(): void{
    const request = new QuestDeleteRequest();
    request.id = this.quest.id;

    this.isRequestInProgress = true;
    this.errorMessage = null;

    this.questService.delete(request).subscribe({
      next: this.handleDeleteSuccess.bind(this),
      error: this.handleError.bind(this)
    });
  }

  private handleApproveCompletionSuccess(): void {
    // TODO: reload quest from server;

    this.isRequestInProgress = false;
  }

  private handleDeleteSuccess(): void {
    this.router.navigateByUrl("/quest/list");

    this.isRequestInProgress = false;
  }

  private handleError(error: HttpErrorResponse): void {
    this.errorMessage = error.status === 0 ? "Connection error" : "Something went wrong";

    this.isRequestInProgress = false;
  }
}
