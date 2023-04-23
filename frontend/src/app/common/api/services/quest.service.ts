import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs';
import {
  QuestApproveCompletionRequest,
  QuestCreateRequest,
  QuestCreateResponse,
  QuestDeleteRequest,
  QuestEditRequest,
  QuestGetAllRequest,
  QuestGetAllResponse,
  QuestGetRequest,
  QuestGetResponse,
  QuestSetProgressRequest
} from '../models/quest';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class QuestService extends BaseService {

  public constructor(private httpClient: HttpClient) {
    super();
  }

  public get(request: QuestGetRequest): Observable<QuestGetResponse> {
    const params = new HttpParams()
      .set("id", request.id);

    return this.httpClient.get<QuestGetResponse>(this.getUrl("/quest/get"), { params })
  }

  public create(request: QuestCreateRequest): Observable<QuestCreateResponse> {
    return this.httpClient.post<QuestCreateResponse>(this.getUrl("/quest/create"), request);
  }

  public edit(request: QuestEditRequest): Observable<unknown> {
    return this.httpClient.put(this.getUrl("/quest/edit"), request);
  }

  public delete(request: QuestDeleteRequest): Observable<unknown> {
    return this.httpClient.delete(this.getUrl("/quest/delete"), { body: request })
  }

  public setProgress(request: QuestSetProgressRequest): Observable<unknown> {
    return this.httpClient.put(this.getUrl("/quest/setProgress"), request);
  }

  public approveCompletion(request: QuestApproveCompletionRequest): Observable<unknown> {
    return this.httpClient.post(this.getUrl("/quest/approveCompletion"), request);
  }

  public getAll(request: QuestGetAllRequest): Observable<QuestGetAllResponse> {
    return this.httpClient.post<QuestGetAllResponse>(this.getUrl("/quest/getAll"), request);
  }
}
