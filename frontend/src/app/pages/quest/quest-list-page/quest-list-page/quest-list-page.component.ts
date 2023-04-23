import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { QuestGetAllRequest, QuestGetAllResponse } from 'src/app/common/api/models/quest';
import { QuestService } from 'src/app/common/api/services';

@Component({
  selector: 'app-quest-list-page',
  templateUrl: './quest-list-page.component.html',
  styleUrls: ['./quest-list-page.component.css']
})
export class QuestListPageComponent implements OnInit {
  public pageIndex: number = 0;
  public pageSize: number = 10;
  public response: QuestGetAllResponse | null = null;
  
  public constructor(private questService: QuestService) { }

  public ngOnInit(): void {
    this.loadQuests();
  }

  public onPageUpdated(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadQuests();
  }

  private loadQuests(): void {
    this.response = null;

    const request = new QuestGetAllRequest();
    request.pageNumber = this.pageIndex + 1;
    request.pageSize = this.pageSize;

    this.questService.getAll(request)
      .subscribe({
        next: response => this.response = response,
      });
  }
}

